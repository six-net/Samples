<template>
  <div class="index-container">
    <table class="table">
      <tr>
        <td>GitHub</td>
        <td>
          <a href="https://github.com/eznew-net" target="_blank">
            https://github.com/eznew-net
          </a>
        </td>
      </tr>
      <tr>
        <td>Gitee</td>
        <td>
          <a href="https://gitee.com/eznew-net" target="_blank">
            https://gitee.com/eznew-net
          </a>
        </td>
      </tr>
    </table>
  </div>
</template>

<script>
  export default {
    name: 'Index',
    components: {},
    data() {
      return {}
    },
    created() {},
    beforeDestroy() {},
    mounted() {},
    methods: {},
  }
</script>
<style lang="scss" scoped>
  .index-container {
    padding: 0 !important;
    margin: 0 !important;
    background: #f5f7f8 !important;

    .card {
      ::v-deep {
        .el-card__body {
          .echarts {
            width: 100%;
            height: 305px;
          }
        }
      }
    }

    .bottom {
      padding-top: 20px;
      margin-top: 5px;
      color: #595959;
      text-align: left;
      border-top: 1px solid $base-border-color;
    }

    .table {
      width: 100%;
      color: #666;
      border-collapse: collapse;
      background-color: #fff;

      td {
        position: relative;
        min-height: 20px;
        padding: 9px 15px;
        font-size: 14px;
        line-height: 20px;
        border: 1px solid #e6e6e6;

        &:nth-child(odd) {
          width: 20%;
          text-align: right;
          background-color: #f7f7f7;
        }
      }
    }

    .icon-panel {
      height: 117px;
      text-align: center;
      cursor: pointer;

      svg {
        font-size: 40px;
      }

      p {
        margin-top: 10px;
      }
    }

    .bottom-btn {
      button {
        margin: 5px 10px 15px 0;
      }
    }
  }
</style>
