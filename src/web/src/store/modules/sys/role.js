import { doGetRoleConfig } from '@/api/sys/role'

const state = () => ({
  config: {},
  hasInit: false,
})
const getters = {
  //获取角色状态文本
  getRoleStatusDisplayText: (state) => (statusValue) => {
    if (state.config && state.config.statusCollection) {
      for (let i in state.config.statusCollection) {
        let item = state.config.statusCollection[i]
        if (item.key == statusValue) {
          return item.value
        }
      }
      return state.config.statusCollection[statusValue]
    }
    return statusValue
  },
  //获取状态字典
  getRoleStatusDict(state) {
    return state.config.statusCollection
  },
  //获取默认状态
  getRoleDefaultStatus(state) {
    if (state.config.statusCollection) {
      return parseInt(state.config.statusCollection[0].key)
    }
    return null
  },
}
const mutations = {
  //设置角色配置数据
  setConfig(state, newConfig) {
    state.config = newConfig
    state.hasInit = true
  },
}
const actions = {
  //刷新配置
  async refreshRoleConfig({ commit }) {
    const { data } = await doGetRoleConfig()
    commit('setConfig', data)
  },

  //初始化配置
  async initRoleConfig({ state, dispatch }) {
    if (!state.hasInit) {
      await dispatch('refreshRoleConfig')
    }
  },
}
export default { state, getters, mutations, actions }
