// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'

import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import axios from 'axios' // axios
import Vant from 'vant'
import 'vant/lib/index.css'

Vue.prototype.$axios = axios // axios

Vue.use(ElementUI) // 全局使用ElementUI

Vue.config.productionTip = false

Vue.use(Vant) // 引用vant组件

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
