import request from '@/utils/request'

//查询操作
export async function doQueryOperation(filter) {
  return await request({
    url: '/operation/query',
    method: 'post',
    data: filter,
  })
}

//保存操作
export async function doSaveOperation(saveInfo) {
  return await request({
    url: '/operation/save',
    method: 'post',
    data: saveInfo,
  })
}

//获取操作配置
export async function doGetOperationConfig() {
  return await request({
    url: '/operation/config',
    method: 'get',
  })
}

//删除操作
export async function doDeleteOperation(keys) {
  return await request({
    url: '/operation/delete',
    method: 'post',
    data: {
      ids: keys,
    },
  })
}

//添加操作权限
export async function doAddOperationPermission(operationPermissions) {
  return await request({
    url: '/operation/permission/add',
    method: 'post',
    data: operationPermissions,
  })
}

//删除操作权限
export async function doDeleteOperationPermission(operationPermissions) {
  return await request({
    url: '/operation/permission/delete',
    method: 'post',
    data: operationPermissions,
  })
}

//清除操作权限
export async function doClearOperationPermission(operationIds) {
  return await request({
    url: '/operation/permission/clear',
    method: 'post',
    data: operationIds,
  })
}
