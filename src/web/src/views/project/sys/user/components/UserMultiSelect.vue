<template>
  <el-dialog
    :title="getTitle"
    :visible.sync="showDialog"
    :close-on-click-modal="false"
    :close-on-press-escape="false"
    :show-close="false"
    :append-to-body="inner"
    width="700px"
    custom-class="multi-select-dialog hideHeader"
    @close="close"
  >
    <el-tabs type="border-card">
      <el-tab-pane label="数据列表">
        <div class="table-container">
          <vab-query-form>
            <vab-query-form-left-panel :span="24">
              <el-form
                ref="form"
                :model="queryForm"
                :inline="true"
                @submit.native.prevent
              >
                <el-form-item>
                  <el-input
                    v-model="queryForm.nameMateKey"
                    placeholder="名称"
                  />
                </el-form-item>
                <el-form-item>
                  <el-button
                    icon="el-icon-search"
                    type="primary"
                    native-type="submit"
                    :disabled="pageStatus.isDisabled"
                    @click="handleQuery"
                  >
                    查询
                  </el-button>
                </el-form-item>
              </el-form>
            </vab-query-form-left-panel>
          </vab-query-form>

          <el-table
            ref="data-list-table"
            v-loading="pageStatus.isLoading"
            :data="dataList"
            :element-loading-text="pageStatus.loadingText"
            :height="tableHeight"
          >
            <el-table-column
              show-overflow-tooltip
              prop="realName"
              label="姓名"
            ></el-table-column>
            <el-table-column
              show-overflow-tooltip
              prop="userName"
              label="登录名"
              width="150"
            ></el-table-column>
            <el-table-column label="状态" width="80">
              <template #default="{ row }">
                <span>
                  {{ getUserStatusDisplayText(row.status) }}
                </span>
              </template>
            </el-table-column>
            <el-table-column
              show-overflow-tooltip
              label="操作"
              align="center"
              width="80px"
            >
              <template #default="{ row }">
                <el-button
                  v-if="hasSelected(row)"
                  type="danger"
                  @click="handleCancelSelectData(row)"
                >
                  移除
                </el-button>
                <el-button v-else type="primary" @click="handleSelectData(row)">
                  选择
                </el-button>
              </template>
            </el-table-column>
          </el-table>

          <ez-pagination :pagination-info="paginationInfo"></ez-pagination>
        </div>
      </el-tab-pane>
      <el-tab-pane label="已选数据">
        <div class="table-container">
          <vab-query-form>
            <vab-query-form-left-panel :span="24">
              <el-button
                icon="el-icon-delete"
                type="danger"
                native-type="submit"
                @click="handleClearSelected"
              >
                清空选择
              </el-button>
            </vab-query-form-left-panel>
          </vab-query-form>
          <el-table
            ref="selected-data-list-table"
            v-loading="pageStatus.isLoading"
            :data="selectedValues"
            :element-loading-text="pageStatus.loadingText"
            :height="selectedTableHeight"
          >
            <el-table-column
              show-overflow-tooltip
              prop="realName"
              label="姓名"
            ></el-table-column>
            <el-table-column
              show-overflow-tooltip
              prop="userName"
              label="登录名"
              width="150"
            ></el-table-column>
            <el-table-column label="状态" width="80">
              <template #default="{ row }">
                <span>
                  {{ getUserStatusDisplayText(row.status) }}
                </span>
              </template>
            </el-table-column>
            <el-table-column
              show-overflow-tooltip
              label="操作"
              align="center"
              width="80px"
            >
              <template #default="{ row }">
                <el-button type="danger" @click="handleCancelSelectData(row)">
                  移除
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
      </el-tab-pane>
    </el-tabs>

    <span slot="footer" class="dialog-footer">
      <el-button @click="showDialog = false">取 消</el-button>
      <el-button
        type="primary"
        :disabled="pageStatus.isDisabled"
        @click="handleConfirmSelected"
      >
        确 定
      </el-button>
    </span>
  </el-dialog>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import { doQueryUser } from '@/api/sys/user'
  import EzPagination from '@/components/EzPagination'
  const { mapActions, mapGetters } = createNamespacedHelpers('sys/user')

  export default {
    name: 'UserMultiSelect',
    components: { EzPagination },
    props: {
      inner: {
        type: Boolean,
        default: () => true,
      },
    },
    data() {
      return {
        //是否显示页面
        showDialog: false,
        //数据列表
        dataList: [],
        //分页信息
        paginationInfo: {},
        //页面状态
        pageStatus: {},
        //查询表单
        queryForm: {
          nameMateKey: '',
        },
        //已选数据
        selectedRows: new Map(),
        selectedKeys: [],
        selectedValues: [],
      }
    },
    computed: {
      ...mapGetters(['getUserStatusDisplayText']),
      //获取标题
      getTitle() {
        return '选择账户'
      },
      //获取表格高度
      tableHeight() {
        return '300px'
      },
      //已选数据表格高度
      selectedTableHeight() {
        return '350px'
      },
      //判断是否已经选中
      hasSelected() {
        var vm = this
        return function (row) {
          return vm.selectedKeys.includes(row.id)
        }
      },
    },
    created() {},
    methods: {
      ...mapActions(['initUserConfig']),
      //页面初始化
      initialize() {
        Object.assign(this.$data, this.$options.data())
        this.paginationInfo = this.$getLwtPaginationInfo?.({
          handleQueryData: this.handleQuery,
        })
        this.pageStatus = this.$getPageStatus?.()
      },
      //查询数据
      async handleQuery(options) {
        this.pageStatus.showPageLoading('正在加载用户...')
        if (options && options.initPage) {
          this.paginationInfo.initPage()
        }
        var searchResult = await doQueryUser(
          this.paginationInfo.getQueryForm(this.queryForm)
        )
        if (searchResult && searchResult.data) {
          this.dataList = searchResult.data.items
          this.paginationInfo.setTotal(searchResult.data.totalCount)
        }
        this.pageStatus.closePageLoading()
      },
      //显示页面
      async showPage() {
        this.initialize()
        this.showDialog = true
        this.pageStatus.showPageLoading('正在加载用户...')
        await this.initUserConfig()
        await this.handleQuery()
        this.pageStatus.closePageLoading()
      },
      //关闭回调
      close() {
        //清除选择数据
        this.handleClearSelected()
      },
      //选择数据
      handleSelectData(row) {
        if (row) {
          this.selectedRows.set(row.id, row)
          this.selectedKeys = Array.from(this.selectedRows.keys())
          this.selectedValues = Array.from(this.selectedRows.values())
        }
      },
      //移除选择数据
      handleCancelSelectData(row) {
        if (row) {
          this.selectedRows.delete(row.id)
          this.selectedKeys = Array.from(this.selectedRows.keys())
          this.selectedValues = Array.from(this.selectedRows.values())
        }
      },
      //清除所有选择数据
      handleClearSelected() {
        this.selectedRows.clear()
        this.selectedKeys = Array.from(this.selectedRows.keys())
        this.selectedValues = Array.from(this.selectedRows.values())
      },
      //确认选择
      handleConfirmSelected() {
        if (this.selectedRows.size > 0) {
          this.$emit(
            'confirm-selected-user',
            Array.from(this.selectedRows.keys())
          )
        }
        this.showDialog = false
      },
    },
  }
</script>

<style lang="scss">
  .multi-select-dialog {
    &.hideHeader {
      .el-dialog__header {
        display: none;
      }
    }
    .el-dialog__body {
      border-top: none;
      padding: 0px;
      .el-pagination {
        padding: 10px 0px;
        margin: 0px;
        border-bottom: 1px solid #dcdfe6;
      }
      .el-tabs--border-card {
        box-shadow: none;
        border-bottom: none;
        .el-tabs__content {
          padding: 0px;
        }
      }
    }
    .vab-query-form {
      margin-top: 10px;
    }
    .el-dialog__footer {
      border-top: none;
    }
  }
</style>
