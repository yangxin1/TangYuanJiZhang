<template>
<!--弃用-->
  <div class="footer">
    <div  class="pull-right pr-5">
      sparkle  © 2020 
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      time: new Date()
    }
  }
}
</script>

<style>
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
  float: right !important;
  font-size: 12px;
  color: #676A6C
}
</style>
