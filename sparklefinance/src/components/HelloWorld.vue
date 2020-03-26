<template>
  <div class="hello">
    <h1>Hello</h1>
    <van-button v-if="isLogin" type="info" @click="toLogin">登录</van-button>
    <h2 v-if="showUser">登录成功</h2>
  </div>
</template>

<script>
import { getToken } from '@/utils/auth'
export default {
  name: 'HelloWorld',
  data () {
    return {
      isLogin: 0,
      showUser: 0
    }
  },
  created () {
    this.checkLogin()
  },
  methods: {
    // 跳转登录
    toLogin () {
      console.log('跳转测试')
      this.$router.push({
        path: '/login',
        name: 'login1'
      })
    },
    // 检测登录
    checkLogin () {
      var token = getToken()
      if (token === undefined) {
        this.isLogin = 1
      } else {
        this.showUser = 1
      }
    }
  }
}
</script>

<style scoped>

</style>
