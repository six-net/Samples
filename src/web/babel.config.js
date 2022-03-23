/**
 * @description babel.config
 */
let plugins = []
if (process.env.NODE_ENV !== 'development') {
  plugins.push('transform-remove-console')
}
module.exports = {
  presets: ['@vue/cli-plugin-babel/preset'],
  plugins,
}
