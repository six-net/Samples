import request from '@/utils/request'

//添加角色权限
export async function doAddRolePermission(rolePermissions) {
  return await request({
    url: '/role-permission/add',
    method: 'post',
    data: rolePermissions,
  })
}

//删除角色权限
export async function doDeleteRolePermission(rolePermissions) {
  return await request({
    url: '/role-permission/delete',
    method: 'post',
    data: rolePermissions,
  })
}

//根据角色数据清除角色权限
export async function doClearByRole(roleIds) {
  return await request({
    url: '/role-permission/clear-by-role',
    method: 'post',
    data: roleIds,
  })
}

//根据角色数据清除角色权限
export async function doClearByPermission(permissionIds) {
  return await request({
    url: '/role-permission/clear-by-permission',
    method: 'post',
    data: permissionIds,
  })
}
