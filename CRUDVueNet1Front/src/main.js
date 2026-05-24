import { createApp } from 'vue'
import App from './App.vue'

// 1. Importar los estilos de los iconos y de Vuetify 🎨
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'
// 2. Importar los creadores de Vuetify
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

// 3. Crear la configuración de Vuetify indicando que use los iconos MDI
const vuetify = createVuetify({
  components,
  directives,
  icons: {
    defaultSet: 'mdi', // Con esto ya sabe que cuando uses un icono, buscará en la fuente MDI
  },
})

const app = createApp(App)

app.use(vuetify)

app.mount('#app')
