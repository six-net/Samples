import request from '@/utils/request'
import { encryptedData } from '@/utils/encrypt'
import { loginRSA } from '@/config'

//登录
export async function doLogin(data) {
  if (loginRSA) {
    data = await encryptedData(data)
    data = data.param
  }
  return await request({
    url: '/account/login',
    method: 'post',
    headers: {
      'Content-Type': 'text/plain',
    },
    data,
  })
}

//获取登录用户信息
export async function doGetLoginUserInfo() {
  return await request({
    url: '/user/current/info',
    method: 'get',
  })
}

//退出登录
export async function doLogout() {
  return await request({
    url: '/account/logout',
    method: 'get',
  })
}

//搜索用户
export async function doQueryUser(searchInfo) {
  return await request({
    url: '/user/query',
    method: 'post',
    data: searchInfo,
  })
}

//删除用户
export async function doDeleteUser(userIds) {
  return await request({
    url: '/user/delete',
    method: 'post',
    data: {
      ids: userIds,
    },
  })
}

//保存用户
export async function doSaveUser(userData) {
  return await request({
    url: '/user/save',
    method: 'post',
    data: userData,
  })
}

//检查用户名称
export async function doCheckUserName(checkInfo) {
  return await request({
    url: '/user/check-user-name',
    method: 'post',
    data: checkInfo,
  })
}

//获取用户配置
export async function doGetUserConfig() {
  return await request({
    url: '/user/config',
    method: 'get',
  })
}
