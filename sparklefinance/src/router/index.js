import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
Vue.use(Router)

export default new Router({
  routes: [
    // 这里设置登陆界面
    {
      path: '/',
      name: 'login1',
      component: () => import('@/views/login/index')
    },
    {
      path: '/hello',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      layout: 'layoutmain',
      path: '/views/account',
      name: 'test',
      component: () => import('@/views/account/index'),
      mate: { role: ['student'] }
    },
    {
      path: '/views/account/accountDetail',
      name: 'accountDetail',
      component: () => import('@/views/account/accountDetail')
    },
    {
      path: '/views/test/index',
      name: 'lab',
      component: () => import('@/views/test/index')
    },
    {
      path: '/views',
      name: 'layoutmain',
      component: () => import('@/views/layout/layoutmain')
    }
  ]
})
