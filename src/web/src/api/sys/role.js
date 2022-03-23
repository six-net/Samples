import request from '@/utils/request'

//获取角色配置
export async function doGetRoleConfig() {
  return await request({
    url: '/role/config',
    method: 'get',
  })
}

//查询角色数据
export async function doQueryRole(searchInfo) {
  return await request({
    url: '/role/query',
    method: 'post',
    data: searchInfo,
  })
}

//删除角色
export async function doDeleteRole(roleIds) {
  return await request({
    url: '/role/delete',
    method: 'post',
    data: {
      ids: roleIds,
    },
  })
}

//保存角色
export async function doSaveRole(roleData) {
  return await request({
    url: '/role/save',
    method: 'post',
    data: roleData,
  })
}

//检查角色名称
export async function doCheckRoleName(checkInfo) {
  return await request({
    url: '/role/check-name',
    method: 'post',
    data: checkInfo,
  })
}
