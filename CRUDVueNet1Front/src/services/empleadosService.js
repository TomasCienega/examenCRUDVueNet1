import api from './api'

export default {
  async getEmpleados(idDep = 0) {
    try {
      const response = await api.get(`/Empleado/ListaEmpleados?idDep=${idDep}`)
      return response.data
    } catch (error) {
      console.error('Error fetching empleados:', error)
      throw error
    }
  },
  async getEmpleado(idEmp) {
    try {
      const response = await api.get(`/Empleado/ObtenerEmpleado/${idEmp}`)
      return response.data
    } catch (error) {
      console.error('Error fetching empleado:', error)
      throw error
    }
  },
  async guardarEmpleado(empleado) {
    try {
      const response = await api.post('/Empleado/GuardarEmpleado', empleado)
      return response.data
    } catch (error) {
      console.error('Error saving empleado:', error)
      throw error
    }
  },
  async editarEmpleado(empleado) {
    try {
      const response = await api.put('/Empleado/EditarEmpleado', empleado)
      return response.data
    } catch (error) {
      console.error('Error editing empleado:', error)
      throw error
    }
  },
  async estadoEmpleado(idEmp) {
    try {
      const response = await api.put(`/Empleado/ActualizarEstadoEmpleado/${idEmp}`)
      return response.data
    } catch (error) {
      console.error('Error changing empleado state:', error)
      throw error
    }
  },
  async eliminarEmpleado(idEmp) {
    try {
      const response = await api.delete(`/Empleado/EliminarEmpleado/${idEmp}`)
      return response.data
    } catch (error) {
      console.error('Error deleting empleado:', error)
      throw error
    }
  },
}
