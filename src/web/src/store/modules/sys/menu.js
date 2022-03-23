import { doGetMenuConfig } from '@/api/sys/menu'

const state = () => ({
  config: {},
  hasInit: false,
})
const getters = {
  //获取菜单状态文本
  getMenuStatusDisplayText: (state) => (statusValue) => {
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
  getMenuStatusDict(state) {
    return state.config.statusCollection
  },
  //获取默认状态
  getMenuDefaultStatus(state) {
    if (state.config.statusCollection) {
      return parseInt(state.config.statusCollection[0].key)
    }
    return null
  },

  //获取用途文本
  getMenuUsageDisplayText: (state) => (usageVal) => {
    if (state.config && state.config.usageCollection) {
      for (let i in state.config.usageCollection) {
        let item = state.config.usageCollection[i]
        if (item.key == usageVal) {
          return item.value
        }
      }
    }
    return usageVal
  },
  //获取状态字典
  getMenuUsageDict(state) {
    return state.config.usageCollection
  },
  //获取默认状态
  getMenuDefaultUsage(state) {
    if (state.config.usageCollection) {
      return parseInt(state.config.usageCollection[0].key)
    }
    return null
  },
}
const mutations = {
  //设置权限配置数据
  setMenuConfig(state, newConfig) {
    state.config = newConfig
    state.hasInit = true
  },
}
const actions = {
  //刷新配置
  async refreshMenuConfig({ commit }) {
    const { data } = await doGetMenuConfig()
    commit('setMenuConfig', data)
  },

  //初始化配置
  async initMenuConfig({ state, dispatch }) {
    if (!state.hasInit) {
      await dispatch('refreshMenuConfig')
    }
  },
}
export default { state, getters, mutations, actions }
