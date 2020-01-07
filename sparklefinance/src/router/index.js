import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/views/account',
      name: 'test',
      component: () => import('@/views/account/index')
    },
    {
      path: '/views/account/accountDetail',
      name: 'accountDetail',
      component: () => import('@/views/account/accountDetail')
    }
  ]
})
