import request from '@/utils/request'

//添加用户权限
export async function doAddUserPermission(userPermissions) {
  return await request({
    url: '/user-permission/add',
    method: 'post',
    data: userPermissions,
  })
}

//删除用户权限
export async function doDeleteUserPermission(userPermissions) {
  return await request({
    url: '/user-permission/delete',
    method: 'post',
    data: userPermissions,
  })
}

//根据用户数据清除用户权限
export async function doClearByUser(userIds) {
  return await request({
    url: '/user-permission/clear-by-user',
    method: 'post',
    data: userIds,
  })
}

//根据用户数据清除用户权限
export async function doClearByPermission(permissionIds) {
  return await request({
    url: '/user-permission/clear-by-permission',
    method: 'post',
    data: permissionIds,
  })
}
