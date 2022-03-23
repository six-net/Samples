import { doGetPermissionConfig } from '@/api/sys/permission'

const state = () => ({
  config: {},
  hasInit: false,
})
const getters = {
  //获取权限状态文本
  getPermissionStatusDisplayText: (state) => (statusValue) => {
    if (state.config && state.config.statusCollection) {
      for (let i in state.config.statusCollection) {
        let item = state.config.statusCollection[i]
        if (item.key == statusValue) {
          return item.value
        }
      }
    }
    return statusValue
  },
  //获取状态字典
  getPermissionStatusDict(state) {
    return state.config.statusCollection
  },
  //获取默认状态
  getPermissionDefaultStatus(state) {
    if (state.config.statusCollection) {
      return parseInt(state.config.statusCollection[0].key)
    }
    return null
  },
}
const mutations = {
  //设置权限配置数据
  setConfig(state, newConfig) {
    state.config = newConfig
    state.hasInit = true
  },
}
const actions = {
  //刷新配置
  async refreshPermissionConfig({ commit }) {
    const { data } = await doGetPermissionConfig()
    commit('setConfig', data)
  },

  //初始化配置
  async initPermissionConfig({ state, dispatch }) {
    if (!state.hasInit) {
      await dispatch('refreshPermissionConfig')
    }
  },
}
export default { state, getters, mutations, actions }
