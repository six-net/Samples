import request from '@/utils/request'

//添加菜单权限
export async function doAddMenuPermission(menuPermissions) {
  return await request({
    url: '/menu-permission/add',
    method: 'post',
    data: menuPermissions,
  })
}

//删除菜单权限
export async function doDeleteMenuPermission(menuPermissions) {
  return await request({
    url: '/menu-permission/delete',
    method: 'post',
    data: menuPermissions,
  })
}

//根据菜单数据清除菜单权限
export async function doClearByMenu(menuIds) {
  return await request({
    url: '/menu-permission/clear-by-menu',
    method: 'post',
    data: menuIds,
  })
}

//根据菜单数据清除菜单权限
export async function doClearByPermission(permissionIds) {
  return await request({
    url: '/menu-permission/clear-by-permission',
    method: 'post',
    data: permissionIds,
  })
}
