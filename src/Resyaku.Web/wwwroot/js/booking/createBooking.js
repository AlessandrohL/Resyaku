import client from '../api/api-client.js'
import { toPascalCaseObject } from '../utilities.js'
import Swal from '/lib/sweetalert2/sweetalert2.esm.all.min.js'

const createReservationForm = document.querySelector('#create-reservation-form')
const selectedTableIdsInput = document.querySelector('#TableIds')

const tablesAccordionContainer = document.querySelector('#tables-accordion-container')
const loadingTablesIndicator = document.querySelector('#loading-tables-indicator')
const tablesInfoMessage = document.querySelector('#tables-info-message')
const selectedTablesContainer = document.querySelector('#selected-tables-container')

const tableSelectionAlert = document.querySelector('#table-selection-alert')
const selectionAlertMessage = tableSelectionAlert.querySelector(':scope > p');
const selectionAlertTitle = tableSelectionAlert.querySelector(':scope > h6');

const alertCustomerNotFound = document.querySelector('#alert-customer-not-found')

const customerFields = ['CustomerDni', 'CustomerName', 'CustomerLastname', 'CustomerEmail', 'CustomerPhone']
const loadingCustomerIndicator = document.querySelector('#loading-customer-indicator')

let tables = []
const selectedTableIds = new Set()
const tableAvailabilityFields = ['BookingDate', 'BookingTime', 'Duration', 'PartySize']

createReservationForm.addEventListener('change', async (e) => {

    if (tableAvailabilityFields.includes(e.target.name)) {
        selectedTableIds.clear()
        showTableSelectionAlert('No se ha seleccionado ninguna mesa todavía.')
        clearTablesAccordionContainer()
        clearSelectedTableBadges()
        showTablesLoader()

        const form = Object.fromEntries(new FormData(createReservationForm).entries())

        try {
            tables = await getAvailableTables(form.BookingDate, form.BookingTime, form.Duration)
        } catch (error) {
            setTableInfoMessage('Ocurrio un error al cargar los datos, intentelo más tarde.', 'danger')
            return
        } finally {
            showTablesLoader(false)
        }

        if (tables.length === 0) {
            setTableInfoMessage('No hay mesas disponibles', 'info')
            return
        } else {
            setTableInfoMessage('')
        }

        const groupedTablesByArea = groupTablesByServiceAreaName(tables)

        tablesAccordionContainer.classList.remove('d-none')

        const serviceAreas = Object.entries(groupedTablesByArea)
            .map(([serviceAreaName, tables], index) => {
                const isFirstItem = index === 0
                return createServiceAreaAccordion(serviceAreaName, tables, isFirstItem)
            })
            .join('')

        tablesAccordionContainer.insertAdjacentHTML('beforeend', serviceAreas)
    }
})

tablesAccordionContainer.addEventListener('click', e => {
    const tableItem = e.target.closest('[data-table-id]')
    if (!tableItem) return

    const { tableId, tableName } = tableItem.dataset

    if (selectedTableIds.has(tableId)) {
        selectedTableIds.delete(tableId)
        updateTableItemStyle(tableItem, false)

        dispatchTableRemovedEvent({ tableId })
        return
    }

    selectedTableIds.add(tableId)
    updateTableItemStyle(tableItem, true)

    dispatchTableAddedEvent({ tableId, tableName })
})

createReservationForm.addEventListener('click', async (e) => {

    if (e.target.matches('#btn-search-customer')) {
        e.target.disabled = true
        toggleCustomerFields()
        showAlertCustomerNotFound(false)

        showCustomerLoader()

        const customerDni = createReservationForm.elements['CustomerDni'].value
        try {
            const customerData = await searchCustomerByDni(customerDni)
            fillCustomerFormFields(toPascalCaseObject(customerData))
        } catch (error) {
            showAlertCustomerNotFound()
        } finally {
            e.target.disabled = false
            toggleCustomerFields(false)
            showCustomerLoader(false)
        }
    }
})

createReservationForm.addEventListener('submit', e => {
    e.preventDefault()

    if (!selectedTableIds.size) {
        Swal.fire({
            title: 'No ha seleccionado ninguna mesa',
            icon: 'error',
        })
        return
    } else {
        selectedTableIdsInput.value = [...selectedTableIds].join(',')
    }

    createReservationForm.submit()
})

const TABLE_SELECTION_CHANGED_EVENT = 'tableSelectionChanged'

document.addEventListener(TABLE_SELECTION_CHANGED_EVENT, (e) => {
    if (selectedTableIds.size === 0) {
        showTableSelectionAlert('No se ha seleccionado ninguna mesa todavía.')
        return
    }

    const totalSelectedCapacity = tables
        .filter(t => selectedTableIds.has(String(t.tableId)))
        .reduce((acc, t) => acc + t.maxCapacity, 0)

    const selectedPartySize = Number(createReservationForm.elements['PartySize']?.value) || 0

    if (selectedPartySize > totalSelectedCapacity) {
        showTableSelectionAlert(`Está intentando asignar <strong>${selectedPartySize} comensales</strong> y seleccionó una capacidad de <strong>${totalSelectedCapacity}</strong>. Intenta agregando otra mesa.`)
        return
    }

    showTableSelectionAlert(`Está asignando <strong>${selectedPartySize}</strong> comensales y usando una capacidad de <strong>${totalSelectedCapacity}</strong>`, false)
})

const TABLE_ADDED_EVENT = 'tableAdded'

document.addEventListener(TABLE_ADDED_EVENT, (e) => {
    const { tableId, tableName } = e.detail

    selectedTablesContainer.insertAdjacentHTML(
        'beforeend',
        createSelectedTableBadge(tableName, tableId))

    dispatchTableSelectionChangedEvent()
})

const TABLE_REMOVED_EVENT = 'tableRemoved'

document.addEventListener(TABLE_REMOVED_EVENT, (e) => {
    const { tableId } = e.detail

    removeSelectedTableBadge(tableId)

    dispatchTableSelectionChangedEvent()
})

async function getAvailableTables(bookingDate, bookingTime, duration) {
    const response = await client.get('/tables/available', {
        params: { bookingDate, bookingTime, duration }
    })

    return response.data
}

function groupTablesByServiceAreaName(tables = []) {
    return tables.reduce((acc, table) => {
        const key = table.serviceAreaName
        if (!acc[key]) {
            acc[key] = []
        }
        acc[key].push(table)
        return acc
    }, {})
}

function createServiceAreaAccordion(serviceAreaName, tables = [], isFirstItem = false) {
    const uniqueId = `collapse-${Math.random().toString(36).substring(2, 10)}`

    const buttonClass = isFirstItem ? 'accordion-button' : 'accordion-button collapsed'
    const collapseClass = isFirstItem ? 'accordion-collapse collapse show' : 'accordion-collapse collapse'

    const tablesItemsTemplate = tables
        .map(table => createTable(table))
        .join('')

    return `
    <div class="accordion-item">
        <h2 class="accordion-header">
            <button class="${buttonClass}" type="button" data-bs-toggle="collapse" data-bs-target="#${uniqueId}" aria-expanded="${isFirstItem}" aria-controls="${uniqueId}">
                <div class="d-flex flex-column align-items-start">
                    <div class="mb-1">${serviceAreaName}</div>
                    <div class="text-success" style="font-size: 0.8rem">${tables.length} disponibles</div>
                </div>
            </button>
        </h2>
        <div id="${uniqueId}" class="${collapseClass}" data-bs-parent="#accordion-tables-container">
            <div class="accordion-body d-flex flex-row flex-wrap gap-3">
                ${tablesItemsTemplate}
            </div>
        </div>
    </div>
    `
}

function createTable(table, selected = false) {
    const selectedClass = selected ? 'border-success bg-success' : 'border-info bg-info'

    return `
    <div data-table-id="${table.tableId}" data-table-name="${table.name}" class="bg-opacity-25 border border-3 ${selectedClass} rounded-4 d-flex flex-column justify-content-center align-items-center table-item" style="width: 100px; height: 70px">
        <div class="text-info fw-bold" style="font-size: 14px">${table.name}</div>
        <div>
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
                <path fill="#3498db" d="M12 4a4 4 0 1 1 0 8a4 4 0 0 1 0-8m0 16s8 0 8-2c0-2.4-3.9-5-8-5s-8 2.6-8 5c0 2 8 2 8 2" />
            </svg>
            <span class="text-info" style="font-size: 14px">${table.minCapacity} - ${table.maxCapacity}</span>
        </div>
    </div>
    `
}

function clearTablesAccordionContainer() {
    tablesAccordionContainer.replaceChildren()
}

function showTablesLoader(show = true) {
    show
        ? loadingTablesIndicator.classList.remove('d-none')
        : loadingTablesIndicator.classList.add('d-none')
}

function showTableSelectionAlert(message, isTitleEnabled = true) {
    isTitleEnabled
        ? selectionAlertTitle.classList.remove('d-none')
        : selectionAlertTitle.classList.add('d-none')

    selectionAlertMessage.innerHTML = message
}

function createSelectedTableBadge(tableName = '', tableId) {
    return `<span class="badge text-bg-primary" data-table-badge-id="${tableId}">${tableName}</span>`
}

function removeSelectedTableBadge(tableId) {
    const badge = selectedTablesContainer.querySelector(`[data-table-badge-id="${tableId}"]`)
    if (badge) badge.remove()
}

function clearSelectedTableBadges() {
    selectedTablesContainer.replaceChildren()
}

async function searchCustomerByDni(dni) {
    const response = await client.get(`/customers/by-dni/${dni}`)
    return response.data
}

function fillCustomerFormFields(customerData) {
    customerFields.forEach(field => {
        if (createReservationForm.elements[field]) {
            createReservationForm.elements[field].value = customerData[field] ?? ''
        }
    })
}

function toggleCustomerFields(disable = true) {
    customerFields.forEach(field => {
        if (createReservationForm.elements[field]) {
            createReservationForm.elements[field].disabled = disable
        }
    })
}

function showCustomerLoader(show = true) {
    show
        ? loadingCustomerIndicator.classList.replace('opacity-0', 'opacity-100')
        : loadingCustomerIndicator.classList.replace('opacity-100', 'opacity-0')
}

function showAlertCustomerNotFound(show = true) {
    show
        ? alertCustomerNotFound.classList.remove('d-none')
        : alertCustomerNotFound.classList.add('d-none')
}

function setTableInfoMessage(message, type = 'info') {
    const classMap = {
        info: 'text-dark-emphasis',
        danger: 'text-danger'
    }

    tablesInfoMessage.textContent = message || ''

    tablesInfoMessage.classList.remove('text-dark-emphasis', 'text-danger');

    if (message) {
        tablesInfoMessage.classList.add(classMap[type] || classMap.info)
        tablesInfoMessage.classList.remove('d-none')
    } else {
        tablesInfoMessage.classList.add('d-none');
    }
}

function updateTableItemStyle(tableElement, isSelected) {
    if (isSelected) {
        tableElement.classList.remove('border-info', 'bg-info')
        tableElement.classList.add('border-success', 'bg-success')
    } else {
        tableElement.classList.remove('border-success', 'bg-success')
        tableElement.classList.add('border-info', 'bg-info')
    }
}

function dispatchTableSelectionChangedEvent() {
    document.dispatchEvent(new CustomEvent(TABLE_SELECTION_CHANGED_EVENT))
}

function dispatchTableAddedEvent(detail) {
    document.dispatchEvent(new CustomEvent(TABLE_ADDED_EVENT, { detail }))
}

function dispatchTableRemovedEvent(detail) {
    document.dispatchEvent(new CustomEvent(TABLE_REMOVED_EVENT, { detail }))
}