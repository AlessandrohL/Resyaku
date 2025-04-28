import axios from '/lib/axios/esm/axios.js'

const client = axios.create({
    baseURL: window.location.origin,
    timeout: 17000,
    //withCredentials: true
})

export default client