import request from '@/utils/request'

//查询菜单
export async function doQueryMenu(filter) {
  return await request({
    url: '/menu/query',
    method: 'post',
    data: filter,
  })
}

//获取菜单配置
export async function doGetMenuConfig() {
  return await request({
    url: '/menu/config',
    method: 'get',
  })
}

//保存菜单
export async function doSaveMenu(saveInfo) {
  return await request({
    url: '/menu/save',
    method: 'post',
    data: saveInfo,
  })
}

//删除菜单
export async function doDeleteMenu(dataKeys) {
  return await request({
    url: '/menu/delete',
    method: 'post',
    data: {
      ids: dataKeys,
    },
  })
}

//添加菜单权限
export async function doAddMenuPermission(menuPermissions) {
  return await request({
    url: '/menu/permission/add',
    method: 'post',
    data: menuPermissions,
  })
}

//删除菜单权限
export async function doDeleteMenuPermission(menuPermissions) {
  return await request({
    url: '/menu/permission/delete',
    method: 'post',
    data: menuPermissions,
  })
}

//清除菜单权限
export async function doClearMenuPermission(menuIds) {
  return await request({
    url: '/menu/permission/clear',
    method: 'post',
    data: menuIds,
  })
}
