<template>
  <!--这是测试页面-->
  <div>
    <h1>LAB</h1>
    <van-button type="warning" @click="removeCookie">删除cookie</van-button>
  </div>
</template>

<script>
import { removeToken } from '@/utils/auth'
import { Notify } from 'vant'
export default {
  name: 'lab',
  data () {
    return {
      active: 0
    }
  },
  methods: {
    // 删除cookie(退出登录)
    removeCookie () {
      removeToken()
      Notify('cookie已经删除')
    }
  }
}
</script>

<style>
</style>
