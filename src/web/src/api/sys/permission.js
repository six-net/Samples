import request from '@/utils/request'

//查询权限
export async function doQueryPermission(filter) {
  return await request({
    url: '/permission/query',
    method: 'post',
    data: filter,
  })
}

//保存权限
export async function doSavePermission(saveInfo) {
  return await request({
    url: '/permission/save',
    method: 'post',
    data: saveInfo,
  })
}

//获取权限配置
export async function doGetPermissionConfig() {
  return await request({
    url: '/permission/config',
    method: 'get',
  })
}

//删除权限
export async function doDeletePermission(keys) {
  return await request({
    url: '/permission/delete',
    method: 'post',
    data: {
      ids: keys,
    },
  })
}

//添加菜单权限
export async function doAddMenuPermission(menuPermissionInfo) {
  return await request({
    url: '/permission/menu/add',
    method: 'post',
    data: menuPermissionInfo,
  })
}
