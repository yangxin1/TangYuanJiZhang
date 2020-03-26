<template>
  <div>
    <!--账本详情-->
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <h2 style="float: left; color:green;">{{form.name}}</h2>
      </div>
      <div>
        <el-form :model="form">
          <el-form-item label="总金额：">
            <el-input v-model="form.amount" placeholder="总金额"></el-input>
          </el-form-item>
          <el-form-item label="账户类型：">
            <br>
            <van-dropdown-menu>
              <van-dropdown-item v-model="form.accountType" :options="typeList" />
            </van-dropdown-menu>
            <!-- <el-select style="float:left" v-model="form.accountType" placeholder="账户类型">
              <el-option v-for="item in typeList" :key="item.value" :label="item.key" :value="item.value"></el-option>
            </el-select> -->
          </el-form-item>
          <el-form-item label="备注">
            <el-input v-model="form.remark" placeholder="备注"></el-input>
          </el-form-item>
        </el-form>
      </div>
    </el-card>
    <div>
      <p>在这里显示该账户消费详情</p>
      <van-list v-model="loading" :finished="finished" finished-text="已经到底了" @load="onload">

      </van-list>

    </div>
  </div>
</template>
<script>
import { getAccountById, getDealList } from '@/api/account/account'
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
      },
      typeList: [{
        text: '储蓄卡账户',
        value: 1
      }, {
        text: '网络账户',
        value: 2
      }],
      deallist: [] // 交易记录列表
    }
  },
  created () {
    this.getAccount()
  },
  methods: {
    // 获取账本详情
    getAccount () {
      this.id = this.$route.params.Id // 获取账本ID
      getAccountById(this.id).then(res => {
        this.form = res.data
      })
    },
    // 获取账本消费记录
    getDeallistbyid (id) {
      getDealList(id, 1, 10)
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
    width: 90%;
    margin: 0 auto;
    align-content: center;
    align-self: center;
  }
</style>
