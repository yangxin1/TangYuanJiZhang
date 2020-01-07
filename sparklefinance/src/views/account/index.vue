<template>
<div>
  <div>
    <!--标签页1-->
    <el-tabs v-model="activeName">
      <el-tab-pane label="最近交易" name="first">
        <p>记一笔</p>
      </el-tab-pane>
      <!--标签页2-->
      <el-tab-pane label="账户管理" name="second">
        <el-card class="box-card" center=true>
          <div slot="header" class="clearfix">
            <span>净资产</span>
            <el-button icon="el-icon-view" style="float: right; padding: 3px 0" type="text" @click="hideMoney">{{hideButton}}</el-button>
          </div>
          <h1 class="amountColor">
            ￥{{showAmount}}
          </h1>
        </el-card>
        <br>
        <div v-for="item in list" :key="item.id">
          <el-card class="box-card">
            <div slot="header" class="clearfix" @click="UpdateAccount(item.id)">
              <span v-if="item.accountType==1">储蓄卡账户</span>
              <span v-else>网络账户</span>
              <el-button style="float: right; padding: 3px 0" type="text" @click="UpdateAccount(item.id)" >查看</el-button>
            </div>
              <el-row>
                <el-col :span="6">
                  <span>{{item.name}}</span>
                </el-col>
                <el-col :span="10">
                  <p></p>
                </el-col>
                <el-col :span="6">
                  <span v-if="hideButton=='隐藏'">￥{{item.amount}}</span>
                  <span v-else>￥***</span>
                </el-col>
              </el-row>
          </el-card>
          <br>
        </div>
      </el-tab-pane>
      <!--标签页3-->
      <el-tab-pane label="统计" name="third">
        <p>统计图表</p>
      </el-tab-pane>
    </el-tabs>
  </div>
</div>
</template>
<script>
import { page } from '@/api/account/account'
export default {
  name: 'test',
  components: {
    // Bottom
  },
  data () {
    return {
      activeName: 'second', // 标签首先显示
      activeIndex: '1', // 导航栏
      dialogVisible: false, // 显示账本弹窗
      list: [], // 后端数据
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
      showAmount: undefined, // 总金额
      hideButton: '隐藏' // 卡片按钮
    }
  },
  created () {
    this.getPage()
  },
  methods: {
    // 获取页面
    getPage () {
      console.log('调用接口')
      page(1, 10).then(res => {
        this.list = res.data
        this.getAllmoney()
      })
    },
    // 获取总金额
    getAllmoney () {
      var amount = 0
      this.list.forEach(el => {
        amount += el.amount
      })
      this.showAmount = amount
    },
    // 隐藏数据
    hideMoney () {
      if (this.showAmount !== '***') {
        this.showAmount = '***'
        this.hideButton = '显示'
      } else {
        this.hideButton = '隐藏'
        this.getAllmoney()
      }
    },
    // 获取账户详细信息
    UpdateAccount (id) {
      // 跳转到详情页面
      this.$router.push({
        path: '/',
        name: 'accountDetail',
        params: {
          Id: id
        }
      }).catch(data => {})
    },
    // 弹窗关闭确认
    handleClose (done) {
      this.$confirm('确认关闭？')
        .then(_ => {
          done()
        }).catch(_ => {})
    }
  }
}
</script>
<style>
  .item {
    margin-bottom: 18px;
  }
  .box-card {
    width: 99%;
    align-content: center;
    align-self: center;
  }
  .amountColor {
    color: green;
  }
</style>
