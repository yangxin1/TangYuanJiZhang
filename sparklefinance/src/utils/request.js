import axios from 'axios'
import { Message } from 'element-ui'
// import store from '@/store'
import { getToken } from '@/utils/auth'

// 创建axios实例
const service = axios.create({
  baseURL: process.env.BASE_API, // api的base_url
  timeout: 5000 // 请求超时时间（原定5s，现为10s）
})
// request拦截器
service.interceptors.request.use(config => {
  config.headers['Authorization'] = 'Bearer ' + getToken() // 让每个请求携带token
  return config
}, error => {
  console.log('发送请求报错：' + error) // for debug
  Promise.reject(error)
})
// respone拦截器
service.interceptors.response.use(
  response => {
  /**
  * 通过response自定义code来标示请求状态，当code返回如下情况为权限有问题，登出并返回到登录页
  * 如通过xmlhttprequest 状态码标识 逻辑可写在下面error中
  */
    const res = response.data
    console.log('状态：linsh' + response.status) // linshi
    // 登出弹窗
    // if (response.status === 401 || (res.code && res.code === 40101)) {
    //   Message({
    //     message: res.message + '请重新登录',
    //     type: 'error',
    //     duration: 5 * 1000
    //   })
    // }
    // if (response.status === 401 || (res.code && res.code === 40101)) {
    //   MessageBox.confirm('你已被登出，可以取消继续留在该页面，或者重新登录', '确定登出', {
    //     confirmButtonText: '重新登录',
    //     cancelButtonText: '取消',
    //     type: 'warning'
    //   }).then(() => {
    //     store.dispatch('FedLogOut').then(() => {
    //       location.reload() // 为了重新实例化vue-router对象 避免bug
    //     })
    //   })
    //   return Promise.reject('error')
    // }
    if (response.code && response.code !== 200) {
      console.log('后端返回400显示：' + response.code)
      Message({
        message: res.message,
        type: 'error',
        duration: 5 * 1000
      })
    } else {
      return response.data
    }
  },
  error => {
    console.log('访问故障：' + error)// for debug
    Message({
      message: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    return Promise.reject(error)
  }
)
export default service
