import request from '@/utils/request'

export function login (obj) {
  var data = request({
    method: 'post',
    url: '/api/login/login',
    data: obj
  })
  // Cookies.set('Token', data.data)
  return data
}
