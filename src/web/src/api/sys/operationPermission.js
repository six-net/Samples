import request from '@/utils/request'

//添加功能权限
export async function doAddOperationPermission(operationPermissions) {
  return await request({
    url: '/operation-permission/add',
    method: 'post',
    data: operationPermissions,
  })
}

//删除功能权限
export async function doDeleteOperationPermission(operationPermissions) {
  return await request({
    url: '/operation-permission/delete',
    method: 'post',
    data: operationPermissions,
  })
}

//根据功能数据清除功能权限
export async function doClearByOperation(operationIds) {
  return await request({
    url: '/operation-permission/clear-by-operation',
    method: 'post',
    data: operationIds,
  })
}

//根据功能数据清除功能权限
export async function doClearByPermission(permissionIds) {
  return await request({
    url: '/operation-permission/clear-by-permission',
    method: 'post',
    data: permissionIds,
  })
}
