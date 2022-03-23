/**
 * @description 登录、获取用户信息、退出登录、清除accessToken逻辑，不建议修改
 */

import Vue from 'vue'
import {
  doGetLoginUserInfo,
  doLogin,
  doLogout,
  doGetUserConfig,
} from '@/api/sys/user'
import {
  getAccessToken,
  removeAccessToken,
  setAccessToken,
} from '@/utils/accessToken'
import { resetRouter } from '@/router'
import { title, tokenName } from '@/config'

//数据
const state = () => ({
  accessToken: getAccessToken(),
  userName: '',
  avatar: '',
  permissions: [],
  config: null,
  hasInit: false,
})

//属性
const getters = {
  //获取token
  accessToken: (state) => state.accessToken,
  //获取用户名
  userName: (state) => state.userName,
  //获取头像
  avatar: (state) => state.avatar,
  //获取权限
  permissions: (state) => state.permissions,
  //获取状态显示文本
  getUserStatusDisplayText: (state) => (statusValue) => {
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
  getUserStatusDict(state) {
    return state.config?.statusCollection
  },
  //获取默认状态
  getUserDefaultStatus(state) {
    if (state.config.statusCollection) {
      return parseInt(state.config.statusCollection[0].key)
    }
    return null
  },
}

//数据修改方法
const mutations = {
  //设置访问token
  setAccessToken(state, accessToken) {
    state.accessToken = accessToken
    setAccessToken(accessToken)
  },
  //设置用户名
  setUsername(state, username) {
    state.username = username
  },
  //设置头像
  setAvatar(state, avatar) {
    state.avatar = avatar
  },
  //设置权限
  setPermissions(state, permissions) {
    state.permissions = permissions
  },
  //设置配置数据
  setConfig(state, newConfig) {
    state.config = newConfig
    state.hasInit = true
  },
}

//功能方法
const actions = {
  //设置用户权限
  setPermissions({ commit }, permissions) {
    commit('setPermissions', permissions)
  },
  //登录
  async login({ commit }, userInfo) {
    const { data } = await doLogin(userInfo)
    const accessToken = data[tokenName]
    if (accessToken) {
      commit('setAccessToken', accessToken)
      const hour = new Date().getHours()
      const thisTime =
        hour < 8
          ? '早上好'
          : hour <= 11
          ? '上午好'
          : hour <= 13
          ? '中午好'
          : hour < 18
          ? '下午好'
          : '晚上好'
      Vue.prototype.$baseNotify(`欢迎登录${title}`, `${thisTime}！`)
    } else {
      Vue.prototype.$baseMessage(
        `登录接口异常，未正确返回${tokenName}...`,
        'error'
      )
    }
  },
  //获取用户信息
  async getUserInfo({ commit, state }) {
    const { data } = await doGetLoginUserInfo(state.accessToken)
    if (!data) {
      Vue.prototype.$baseMessage('验证失败，请重新登录...', 'error')
      return false
    }
    let { permissions, userName, avatar } = data
    if (permissions && userName && Array.isArray(permissions)) {
      commit('setPermissions', permissions)
      commit('setUsername', userName)
      commit('setAvatar', avatar)
      return permissions
    } else {
      Vue.prototype.$baseMessage('用户信息接口异常', 'error')
      return false
    }
  },
  //退出登录
  async logout({ dispatch }) {
    await doLogout(state.accessToken)
    await dispatch('resetAccessToken')
    await resetRouter()
  },
  //重置用户token
  resetAccessToken({ commit }) {
    commit('setPermissions', [])
    commit('setAccessToken', '')
    removeAccessToken()
  },
  //刷新配置
  async refreshUserConfig({ commit }) {
    const { data } = await doGetUserConfig()
    commit('setConfig', data)
  },
  //初始化配置
  async initUserConfig({ state, dispatch }) {
    if (!state.hasInit) {
      await dispatch('refreshUserConfig')
    }
  },
}
export default { state, getters, mutations, actions }
