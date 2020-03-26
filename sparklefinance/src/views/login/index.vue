<template>
  <!--用户登录-->
  <div>
    <h1>登录</h1>
    <van-cell-group>
      <van-field
        v-model="form.name"
        required
        clearable
        label="用户名"
        right-icon="question-o"
        placeholder="请输入用户名"
        @click-right-icon="$toast('请输入手机号/用户名/邮箱')"
      />
      <van-field
        v-model="form.password"
        type="password"
        label="密码"
        placeholder="请输入密码"
        required
      />
      <br>
      <van-button type="info" @click="userLogin">登录</van-button>
      <br>
    </van-cell-group>
  </div>
</template>
<script>
import { login } from '@/api/login/logincheck'
import { Notify } from 'vant'
import { setToken } from '@/utils/auth'

export default {
  name: 'login',
  components: {},
  data () {
    return {
      form: {
        name: undefined,
        password: undefined
      },
      token: undefined,
      show: false,
      loginmsg: ''
    }
  },
  created () {
  },
  methods: {
    // 登录
    userLogin () {
      login(this.form).then(res => {
        if (res.code === 200) {
          // 进行成功判断
          this.token = res.data
          // 保存token
          setToken(this.token)
          this.$router.push({
            path: '/views',
            name: 'layoutmain'
          }).catch(data => {})
        } else {
          this.loginmsg = res.msg
          this.show = true
          Notify(this.loginmsg)
        }
      })
    }
  }
}
</script>
<style scoped>

</style>
