import request from '@/utils/request'

//查询功能分组
export async function doQueryOperationGroup(filter) {
  return await request({
    url: '/operationGroup/query',
    method: 'post',
    data: filter,
  })
}

//保存功能组
export async function doSaveOperationGroup(saveInfo) {
  return await request({
    url: '/operationGroup/save',
    method: 'post',
    data: saveInfo,
  })
}

//检查分组名称
export async function doCheckOperationGroupName(checkInfo) {
  return await request({
    url: '/operationGroup/check-name',
    method: 'post',
    data: checkInfo,
  })
}

//删除分组
export async function doDeleteOperationGroup(groupIds) {
  return await request({
    url: '/operationGroup/delete',
    method: 'post',
    data: {
      ids: groupIds,
    },
  })
}
