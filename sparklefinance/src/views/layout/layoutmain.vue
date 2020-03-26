<template>
  <div id="app">
    <router-view/>
    <!--底部菜单栏-->
    <layout></layout>
  </div>
</template>

<script>
import layout from '@/views/layout/main'
export default {
  name: 'layoutmain',
  components: {
    layout
  },
  data () {
    return {
      active: 0
    }
  },
  methods: {
    // 监听事件
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

</style>
