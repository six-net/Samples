<template>
  <el-pagination
    :small="paginationInfo.small"
    :background="paginationInfo.background"
    :current-page="paginationInfo.currentPage"
    :layout="paginationInfo.layout"
    :page-size="paginationInfo.pageSize"
    :total="paginationInfo.total"
    :disabled="paginationInfo.disabled"
    :pager-count="paginationInfo.paginationInfo"
    :page-count="paginationInfo.pageCount"
    :page-sizes="paginationInfo.pageSizes"
    :popper-class="paginationInfo.popperClass"
    :prev-text="paginationInfo.prevText"
    :next-text="paginationInfo.nextText"
    :hide-on-single-page="paginationInfo.hideOnSinglePage"
    @current-change="handleCurrentPageChange"
    @size-change="handleSizeChange"
  ></el-pagination>
</template>

<script>
  export default {
    name: 'EzPagination',
    props: {
      /* 分页信息 */
      paginationInfo: {
        type: Object,
        default: () => {
          return this.$getPaginationInfo?.()
        },
      },
    },
    data() {
      return {}
    },
    created() {},
    mounted() {},
    methods: {
      //当前页修改触发
      handleCurrentPageChange(val) {
        this.paginationInfo.handleCurrentPageChange(val)
      },

      //单页数据条数调整
      handleSizeChange(val) {
        this.paginationInfo.handleSizeChange(val)
      },
    },
  }
</script>
