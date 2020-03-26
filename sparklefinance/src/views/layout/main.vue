<template>
  <!--模板页-->
  <div id="app">
    <!-- <router-view/> -->
    <van-tabbar v-model="active" @change="onChange">
      <van-tabbar-item icon="home-o">记账</van-tabbar-item>
      <van-tabbar-item icon="search">账本</van-tabbar-item>
      <van-tabbar-item icon="friends-o">LAB</van-tabbar-item>
      <!-- <van-tabbar-item icon="setting-o" disabled>设置</van-tabbar-item> -->
    </van-tabbar>
  </div>
</template>

<script>
export default {
  name: 'layout',
  data () {
    return {
      active: 0
    }
  },
  methods: {
    // 监听事件
    onChange (index) {
      // 主页
      if (index === 0) {
        console.log('执行跳转到第0个菜单')
        this.$router.push({
          path: '/',
          name: 'HelloWorld'
        }).catch(data => {})
      }
      // 菜单1
      if (index === 1) {
        console.log('执行跳转到第1个菜单')
        this.$router.push({
          path: '/',
          name: 'test'
        }).catch(data => {})
      }
      // 菜单2
      if (index === 2) {
        console.log('执行跳转到第2个菜单')
        this.$router.push({
          path: '/',
          name: 'lab'
        }).catch(data => {})
      }
    }
  }
}
</script>

<style>
</style>
