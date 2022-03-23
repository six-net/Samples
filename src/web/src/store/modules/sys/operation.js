import { doGetOperationConfig } from '@/api/sys/operation'

const state = () => ({
  config: {},
  hasInit: false,
})
const getters = {
  //获取操作功能状态文本
  getOperationStatusDisplayText: (state) => (statusValue) => {
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
  getOperationStatusDict(state) {
    return state.config.statusCollection
  },
  //获取默认状态
  getOperationDefaultStatus(state) {
    if (state.config.statusCollection) {
      return parseInt(state.config.statusCollection[0].key)
    }
    return null
  },
  //获取操作功能访问级别文本
  getOperationAccessLevelDisplayText: (state) => (levelValue) => {
    if (state.config && state.config.accessLevelCollection) {
      for (let i in state.config.accessLevelCollection) {
        let item = state.config.accessLevelCollection[i]
        if (item.key == levelValue) {
          return item.value
        }
      }
    }
    return levelValue
  },
  //获取状态字典
  getOperationAccessLevelDict(state) {
    return state.config.accessLevelCollection
  },
  //获取默认状态
  getOperationDefaultAccessLevel(state) {
    if (state.config.accessLevelCollection) {
      return parseInt(state.config.accessLevelCollection[0].key)
    }
    return null
  },
}
const mutations = {
  //设置操作功能配置数据
  setConfig(state, newConfig) {
    state.config = newConfig
    state.hasInit = true
  },
}
const actions = {
  //刷新配置
  async refreshOperationConfig({ commit }) {
    const { data } = await doGetOperationConfig()
    commit('setConfig', data)
  },

  //初始化配置
  async initOperationConfig({ state, dispatch }) {
    if (!state.hasInit) {
      await dispatch('refreshOperationConfig')
    }
  },
}
export default { state, getters, mutations, actions }
