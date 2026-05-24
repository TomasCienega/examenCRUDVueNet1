import api from './api'

export default {
  async login(credenciales) {
    try {
      const response = await api.post('/Usuario/Login', credenciales)
      return response.data
    } catch (error) {
      console.error('Error during login:', error)
      throw error
    }
  },
}
