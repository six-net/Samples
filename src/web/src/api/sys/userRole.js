import request from '@/utils/request'

//添加用户角色
export async function doAddUserRole(userRoles) {
  return await request({
    url: '/user-role/add',
    method: 'post',
    data: userRoles,
  })
}
//移除用户角色
export async function doDeleteUserRole(userRoles) {
  return await request({
    url: '/user-role/delete',
    method: 'post',
    data: userRoles,
  })
}
//清除用户角色
export async function doClearByUser(userIds) {
  return await request({
    url: '/user-role/clear-by-user',
    method: 'post',
    data: userIds,
  })
}
//清除角色用户
export async function doClearByRole(roleIds) {
  return await request({
    url: '/user-role/clear-by-role',
    method: 'post',
    data: roleIds,
  })
}
