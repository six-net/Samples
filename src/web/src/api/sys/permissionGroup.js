import request from '@/utils/request'

//查询权限分组
export async function doQueryPermissionGroup(filter) {
  return await request({
    url: '/permissionGroup/query',
    method: 'post',
    data: filter,
  })
}

//保存权限分组
export async function doSavePermissionGroup(saveInfo) {
  return await request({
    url: '/permissionGroup/save',
    method: 'post',
    data: saveInfo,
  })
}

//检查分组名称
export async function doCheckPermissionGroupName(checkInfo) {
  return await request({
    url: '/permissionGroup/check-name',
    method: 'post',
    data: checkInfo,
  })
}

//删除分组
export async function doDeletePermissionGroup(groupIds) {
  return await request({
    url: '/permissionGroup/delete',
    method: 'post',
    data: {
      ids: groupIds,
    },
  })
}
