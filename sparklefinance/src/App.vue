<template>
  <div id="app">
    <!--导航栏-->
    <el-menu :default-active="activeIndex" class="el-menu-demo" mode="horizontal" @select="handleSelect">
      <el-menu-item index="1">记账</el-menu-item>
      <el-menu-item index="2" @click="showName">其他</el-menu-item>
      <el-menu-item index="3" disabled>个人</el-menu-item>
    </el-menu>
    <router-view/>
    <!--底部-->
    <div  class="pull-right pr-5">
      --------- sparkle  © 2020 ---------
    </div>
  </div>
</template>

<script>
export default {
  name: 'App',
  data () {
    return {
      activeIndex: '2' // 导航栏
    }
  },
  methods: {
    // 点击其他跳转
    showName () {
      this.$router.push({
        path: '/',
        name: 'HelloWorld'
      }).catch(data => {})
    },
    // 导航栏跳转账本页面
    handleSelect (key, keyPath) {
      console.log('导航栏id：' + key)
      if (key === '1') {
        console.log('执行跳转到第一个菜单')
        this.$router.push({
          path: '/',
          name: 'test'
        }).catch(data => {})
      }
    },
    // init
    initShow () {
      this.$router.push({
        path: '/',
        name: 'test'
      }).catch(data => {})
    }
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
  .footer {
  background: none repeat scroll 0 0 #fff;
  border-top: 1px solid #e7eaec;
  overflow: hidden;
  height: 30px;
  position: absolute;
  bottom: 0;
  width: 100%;
  }
  .pull-right {
  margin-top: 7px;
  /* float: right !important;*/
  font-size: 12px;
  color: #676A6C
  }
</style>
