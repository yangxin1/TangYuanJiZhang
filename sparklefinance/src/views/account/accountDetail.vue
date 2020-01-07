<template>
  <div>
    <!--账本详情-->
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <h2 style="float: left; color:green;">{{form.name}}</h2>
        <!-- <span style="float: right; padding: 3px 50," type="text">{{form.amount}}</span> -->
      </div>
      <div>
        <h4 style="float:left;">总金额</h4>
        <el-input v-model="form.amount" placeholder="总金额"></el-input>
        <!-- <p style="float:right;">XX</p> -->
      </div>
    </el-card>
  </div>
</template>
<script>
import { getAccountById } from '@/api/account/account'
export default {
  name: 'accountDetail',
  components: {},
  data () {
    return {
      id: undefined,
      form: {
        name: undefined,
        accountType: undefined,
        amount: undefined,
        isAllAmount: undefined,
        id: undefined,
        userId: undefined,
        createTime: undefined,
        remark: undefined
      }
    }
  },
  created () {
    this.getAccount()
  },
  methods: {
    // 获取账本详情
    getAccount () {
      var id = this.$route.params.Id // 获取账本ID
      getAccountById(id).then(res => {
        this.form = res.data
      })
    }
  }
}
</script>
<style scoped>
  .text {
    font-size: 14px;
    font-family: 'Times New Roman', Times, serif;
  }

  .item {
    margin-bottom: 18px;
  }
  .clearfix:before,
  .clearfix:after {
    display: table;
    content: "";
  }
  .clearfix:after {
    clear: both
  }
  .box-card {
    width: 99%;
    align-content: center;
    align-self: center;
  }
</style>
