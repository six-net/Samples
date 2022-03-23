/**
 * @description router全局配置，如有必要可分文件抽离，其中asyncRoutes只有在intelligence模式下才会用到，vip文档中已提供路由的基础图标与小清新图标的配置方案，请仔细阅读
 */

import Vue from 'vue'
import VueRouter from 'vue-router'
import Layout from '@/layouts'
import EmptyLayout from '@/layouts/EmptyLayout'
import { publicPath, routerMode } from '@/config'

Vue.use(VueRouter)
export const constantRoutes = [
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true,
  },
  {
    path: '/401',
    name: '401',
    component: () => import('@/views/401'),
    hidden: true,
  },
  {
    path: '/404',
    name: '404',
    component: () => import('@/views/404'),
    hidden: true,
  },
]

export const asyncRoutes = [
  {
    path: '/',
    component: Layout,
    redirect: '/index',
    children: [
      {
        path: 'index',
        name: 'Index',
        component: () => import('@/views/index/index'),
        meta: {
          title: '首页',
          icon: 'home',
          affix: true,
        },
      },
    ],
  },
  {
    path: '/sys',
    component: Layout,
    redirect: 'noRedirect',
    name: 'sys',
    meta: {
      title: '系统管理',
      icon: 'cogs',
    },
    children: [
      {
        path: 'role',
        name: 'Role',
        component: () => import('@/views/project/sys/role/index'),
        meta: {
          title: '角色',
          icon: 'users',
          permissions: ['admin'],
        },
      },
      {
        path: 'user',
        name: 'user',
        component: () => import('@/views/project/sys/user/index'),
        meta: {
          title: '用户',
          icon: 'user',
          permissions: ['admin'],
        },
      },
      {
        path: 'permission',
        name: 'permission',
        component: () => import('@/views/project/sys/permission/index'),
        meta: {
          title: '权限',
          icon: 'check-square',
          permissions: ['admin'],
        },
      },
      {
        path: 'menu',
        name: 'menu',
        component: () => import('@/views/project/sys/menu/index'),
        meta: {
          title: '菜单',
          icon: 'list',
          permissions: ['admin'],
        },
      },
      {
        path: 'func',
        name: 'func',
        component: () => import('@/views/project/sys/func/index'),
        meta: {
          title: '功能',
          icon: 'th-large',
          permissions: ['admin'],
        },
      },
    ],
  },
  {
    path: '*',
    redirect: '/404',
    hidden: true,
  },
]

const router = new VueRouter({
  base: publicPath,
  mode: routerMode,
  scrollBehavior: () => ({
    y: 0,
  }),
  routes: constantRoutes,
})

export function resetRouter() {
  location.reload()
}

export default router
