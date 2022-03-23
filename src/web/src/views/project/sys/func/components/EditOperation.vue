<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    :close-on-click-modal="false"
    :close-on-press-escape="false"
    :show-close="false"
    width="650px"
    @close="close"
  >
    <el-form
      ref="form"
      :model="form"
      :rules="rules"
      :inline="true"
      :status-icon="true"
      label-width="80px"
      style="padding: 0px"
    >
      <el-form-item label="分组" prop="group">
        <el-select
          ref="singleTree"
          v-model="selectedGroupName"
          class="vab-tree-select"
          clearable
          popper-class="select-tree-popper"
          value-key="id"
        >
          <el-option :value="form.group">
            <el-tree
              id="singleSelectTree"
              ref="singleSelectTree"
              :current-node-key="form.group"
              :data="operationGroupTreeData"
              :highlight-current="true"
              :expand-on-click-node="false"
              :check-strictly="true"
              node-key="id"
              @node-click="selectTreeNodeClick"
            >
              <template #defalut="{ node }" class="vab-custom-tree-node">
                <span class="vab-tree-item">{{ node.label }}</span>
              </template>
            </el-tree>
          </el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="名称" prop="name">
        <el-input v-model.trim="form.name" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="Controller" prop="controller">
        <el-input v-model.trim="form.controller" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="Action" prop="action">
        <el-input v-model.trim="form.action" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="地址" prop="path">
        <el-input v-model.trim="form.path" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="访问级别" prop="accessLevel">
        <el-select v-model="form.accessLevel" placeholder="访问级别">
          <el-option
            v-for="item in getOperationAccessLevelDict"
            :key="item.key"
            :label="item.value"
            :value="parseInt(item.key)"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="状态" prop="status">
        <el-select v-model="form.status" placeholder="请选择状态">
          <el-option
            v-for="item in getOperationStatusDict"
            :key="item.key"
            :label="item.value"
            :value="parseInt(item.key)"
          ></el-option>
        </el-select>
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="close">取 消</el-button>
      <el-button
        type="primary"
        :loading="pageStatus.isLoading"
        :disabled="pageStatus.isDisabled"
        @click="save"
      >
        保 存
      </el-button>
    </div>
  </el-dialog>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import { doQueryOperationGroup } from '@/api/sys/operationGroup'
  import { doSaveOperation } from '@/api/sys/operation'
  const { mapActions, mapGetters } = createNamespacedHelpers('sys/operation')

  export default {
    name: 'EditOperation',
    data() {
      return {
        //页面状态
        pageStatus: this.$getPageStatus?.(),
        //树节点
        operationGroupTreeData: [],
        operationGroupDataDict: {},
        //选择分组名称
        selectedGroupName: '',
        form: {
          id: 0,
          name: '',
          controller: '',
          action: '',
          path: '',
          status: 0,
          accessLevel: 0,
          group: '0',
          remark: '',
        },
        rules: {
          name: [
            {
              required: true,
              min: 1,
              max: 50,
              trigger: 'blur',
              message: '请输入50个字符内的名称',
            },
            {
              validator: this.handleCheckGroupName,
              trigger: 'blur',
            },
          ],
          remark: [
            {
              required: false,
              max: 50,
              trigger: 'blur',
              message: '请填写50个字符内的备注信息',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
      }
    },
    computed: {
      ...mapGetters([
        'getOperationStatusDisplayText',
        'getOperationStatusDict',
        'getOperationDefaultStatus',
        'getOperationAccessLevelDisplayText',
        'getOperationAccessLevelDict',
        'getOperationDefaultAccessLevel',
      ]),
    },
    created() {},
    methods: {
      ...mapActions(['initConfig']),
      //获取当前分组
      async handleQueryCurrentOperationGroup() {
        this.pageStatus.showPageLoading('正在加载权限分组...')
        this.operationGroupDataDict = {}
        var queryResult = await doQueryOperationGroup({})
        if (queryResult && queryResult.success) {
          this.operationGroupTreeData = this.convertToTreeData(
            queryResult.data,
            '0'
          )
        }
        this.pageStatus.closePageLoading()
      },
      //转换分组数据
      convertToTreeData(groupDatas, parentId) {
        let treeDatas = []
        if (groupDatas && groupDatas.length > 0) {
          let vm = this
          var topGroupDatas = groupDatas.filter((item) => {
            vm.operationGroupDataDict[item.id] = item
            return item.parent == parentId
          })
          topGroupDatas.map((gd) => {
            let nodeData = {
              id: gd.id,
              label: gd.name,
            }
            let childDatas = this.convertToTreeData(groupDatas, gd.id)
            nodeData.children = childDatas
            treeDatas.push(nodeData)
          })
        }
        return treeDatas
      },
      //显示编辑页面
      async showEditOperation(data) {
        this.dialogFormVisible = true
        await this.initConfig()
        if (!data) {
          this.title = '添加接口'
          this.selectedGroupName = ''
          this.form = this.$options.data().form
          this.form.status = this.getOperationDefaultStatus
          this.form.accessLevel = this.getOperationDefaultAccessLevel
        } else {
          this.title = '编辑接口'
          this.form = Object.assign({}, data)
        }
        await this.handleQueryCurrentOperationGroup()
        let groupData = this.operationGroupDataDict[this.form.group]
        if (groupData) {
          this.selectedGroupName = groupData.name
        }
      },
      //选择节点
      selectTreeNodeClick(data, node, el) {
        this.selectedGroupName = data.label
        this.form.group = data.id
        this.$refs.singleTree.blur()
      },
      close() {
        this.$refs['form'].resetFields()
        this.form = this.$options.data().form
        this.dialogFormVisible = false
      },
      //保存分组
      save() {
        this.$refs['form'].validate(async (valid) => {
          if (valid) {
            this.pageStatus.showPageLoading('正在保存权限...')
            const { success, msg } = await doSaveOperation(this.form)
            if (success) {
              this.$baseMessage('保存成功', 'success')
              this.$refs['form'].resetFields()
              this.dialogFormVisible = false
              this.$emit('fetch-operation')
              this.form = this.$options.data().form
            }
            this.pageStatus.closePageLoading()
            return success
          } else {
            return false
          }
        })
      },
    },
  }
</script>

<style>
  .el-dialog__body {
    padding-bottom: 0px;
    padding-top: 20px;
  }
  .el-input__inner {
    padding-right: 0px !important;
  }
</style>
