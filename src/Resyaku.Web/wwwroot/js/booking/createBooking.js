import { toPascalCaseObject } from '../utilities.js'
import axios from '/lib/axios/esm/axios.js'
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
const tableAvailabilityFields = ['BookingDate', 'BookingTime', 'Duration', 'GuestCount']


async function fetchAvailableTables(bookingDate, bookingTime, duration) {
    try {
        const response = await axios({
            url: 'https://localhost:7017/tables/available',
            method: 'get',
            params: {
                bookingDate,
                bookingTime,
                duration
            },
            timeout: 15000
        })
        return response.data
    }
    catch (error) {
        console.log(`Error fetchAvailableTables`, error)
        throw error
    }
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

function createServiceAreaAccordionItem(serviceAreaName, tables = [], isFirstItem = false) {
    const uniqueId = `collapse-${Math.random().toString(36).substring(2, 10)}`

    const buttonClass = isFirstItem ? 'accordion-button' : 'accordion-button collapsed'
    const collapseClass = isFirstItem ? 'accordion-collapse collapse show' : 'accordion-collapse collapse'

    const tablesItemsTemplate = tables
        .map(t => createTableItem(t))
        .join('')

    const template = [
        `<div class="accordion-item">`,
        `   <h2 class="accordion-header">`,
        `       <button class="${buttonClass}" type="button" data-bs-toggle="collapse" data-bs-target="#${uniqueId}" aria-expanded="${isFirstItem}" aria-controls="${uniqueId}">`,
        `           <div class="d-flex flex-column align-items-start">`,
        `               <div class="mb-1">${serviceAreaName}</div>`,
        `               <div class="text-success" style="font-size: 0.8rem">${tables.length} disponibles</div>`,
        `           </div>`,
        `       </button>`,
        `   </h2>`,
        `   <div id="${uniqueId}" class="${collapseClass}" data-bs-parent="#accordion-tables-container">`,
        `       <div class="accordion-body d-flex flex-row flex-wrap gap-3">`,
        tablesItemsTemplate,
        `       </div>`,
        `   </div>`,
        `</div>`
    ].join('')

    const accordionItem = document.createRange().createContextualFragment(template).firstElementChild
    return accordionItem
}

function createTableItem({ tableId, tableName, minCapacity, maxCapacity }, selected = false) {
    const selectedClass = selected ? 'border-success bg-success' : 'border-info bg-info'
    return [
        `<div data-table-id="${tableId}" data-table-name="${tableName}" class="bg-opacity-25 border border-3 ${selectedClass} rounded-4 d-flex flex-column justify-content-center align-items-center table-item" style="width: 100px; height: 70px">`,
        `   <div class="text-info fw-bold" style="font-size: 14px">${tableName}</div>`,
        `   <div>`,
        `       <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><path fill="#3498db" d="M12 4a4 4 0 1 1 0 8a4 4 0 0 1 0-8m0 16s8 0 8-2c0-2.4-3.9-5-8-5s-8 2.6-8 5c0 2 8 2 8 2" /></svg>`,
        `       <span class="text-info" style="font-size: 14px">${minCapacity} - ${maxCapacity}</span>`,
        `   </div>`,
        `</div>`
    ].join('')
}

function getSelectedTableIds() {
    if (!selectedTableIdsInput.value) return new Set()

    return new Set(selectedTableIdsInput.value
        .split(',')
        .filter(id => id)
    )
}

function removeSelectedTableIds() {
    selectedTableIdsInput.value = ''
}

function showTablesLoader(show = true) {
    if (show) {
        loadingTablesIndicator.classList.remove('d-none')
    } else {
        loadingTablesIndicator.classList.add('d-none')
    }
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
    selectedTablesContainer.innerHTML = ''
}

async function searchCustomerByDni(dni) {
    try {
        const response = await axios({
            url: `https://localhost:7017/customers/by-dni/${dni}`,
            method: 'get',
            timeout: 15000
        })
        return response.data
    } catch (error) {
        console.log('Error obtaining the customer by DNI')
        throw error
    }
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


createReservationForm.addEventListener('change', async (e) => {

    if (tableAvailabilityFields.includes(e.target.name)) {
        removeSelectedTableIds()
        clearSelectedTableBadges()
        showTablesLoader()
        const form = Object.fromEntries(new FormData(createReservationForm).entries())

        try {
            tables = await fetchAvailableTables(form.BookingDate, form.BookingTime, form.Duration)
        } catch (error) {
            console.log(error)
            tablesInfoMessage.textContent = 'Ocurrio un error al cargar los datos, intentelo más tarde.'
            tablesInfoMessage.classList.add('text-danger')
            tablesInfoMessage.classList.remove('d-none')
            return
        } finally {
            showTablesLoader(false)
        }

        if (!tables.length) {
            tablesInfoMessage.textContent = 'No hay mesas disponibles.'
            tablesInfoMessage.classList.remove('d-none')
            return
        }

        const groupedTablesByArea = groupTablesByServiceAreaName(tables)

        tablesAccordionContainer.classList.remove('d-none')
        tablesAccordionContainer.innerHTML = ''

        Object.entries(groupedTablesByArea).forEach(([serviceAreaName, tables], index) => {
            const isFirstItem = index === 0
            const accordionItem = createServiceAreaAccordionItem(serviceAreaName, tables, isFirstItem)
            tablesAccordionContainer.appendChild(accordionItem)
        })
    }

    if (e.target.matches('#TableIds') || tableAvailabilityFields.includes(e.target.name)) {
        const selectedTablesIds = getSelectedTableIds()

        if (!selectedTablesIds.size) {
            showTableSelectionAlert('No se ha seleccionado ninguna mesa todavía.')
            return
        }

        const totalSelectedCapacity = tables
            .filter(t => selectedTablesIds.has(String(t.tableId)))
            .reduce((acc, t) => acc + t.maxCapacity, 0)

        const selectedDinersCount = Number(createReservationForm.elements['GuestCount']?.value) || 0

        if (selectedDinersCount > totalSelectedCapacity) {
            showTableSelectionAlert(`Está intentando asignar <strong>${selectedDinersCount} comensales</strong> y seleccionó una capacidad de <strong>${totalSelectedCapacity}</strong>. Intenta agregando otra mesa.`)
            return
        }

        showTableSelectionAlert(`Está asignando <strong>${selectedDinersCount}</strong> comensales y usando una capacidad de <strong>${totalSelectedCapacity}</strong>`, false)
    }
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

tablesAccordionContainer.addEventListener('click', e => {
    const tableItem = e.target.closest('[data-table-id]')
    if (!tableItem) return

    const selectedTableIds = getSelectedTableIds()
    const tableId = tableItem.dataset.tableId;
    const tableName = tableItem.dataset.tableName;

    if (selectedTableIds.has(tableId)) {
        selectedTableIds.delete(tableId)
        removeSelectedTableBadge(tableId)
    } else {
        selectedTableIds.add(tableId)
        selectedTablesContainer.innerHTML += createSelectedTableBadge(tableName, tableId)
    }

    selectedTableIdsInput.value = [...selectedTableIds].join(',')
    selectedTableIdsInput.dispatchEvent(new Event('change', { bubbles: true }))

    tableItem.classList.toggle('border-info')
    tableItem.classList.toggle('bg-info')
    tableItem.classList.toggle('border-success')
    tableItem.classList.toggle('bg-success')
})

createReservationForm.addEventListener('submit', e => {
    e.preventDefault()

    if (!getSelectedTableIds().size) {
        Swal.fire({
            title: 'No ha seleccionado ninguna mesa',
            icon: 'error',
        })
        return
    }

    createReservationForm.submit()
})



