import vuetify from 'eslint-config-vuetify'

export default [
  ...vuetify(),
  {
    rules: {
      'vue/multi-word-component-names': 'off'
    }
  }
]
