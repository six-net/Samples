<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    :close-on-click-modal="false"
    :close-on-press-escape="false"
    :show-close="false"
    width="500px"
    @close="close"
  >
    <el-form
      ref="form"
      :model="form"
      :rules="rules"
      :status-icon="true"
      label-width="80px"
    >
      <el-form-item label="上级" prop="parent">
        <el-select
          ref="singleTree"
          v-model="selectedGroupName"
          class="vab-tree-select"
          clearable
          popper-class="select-tree-popper"
          value-key="id"
        >
          <el-option :value="form.parent">
            <el-tree
              id="singleSelectTree"
              ref="singleSelectTree"
              :current-node-key="form.parent"
              :data="permissionGroupTreeData"
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
      <el-form-item label="备注" prop="remark">
        <el-input v-model="form.remark" type="textarea"></el-input>
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
  import {
    doQueryPermissionGroup,
    doSavePermissionGroup,
    doCheckPermissionGroupName,
  } from '@/api/sys/permissionGroup'
  export default {
    name: 'EditPermissionGroup',
    data() {
      return {
        //页面状态
        pageStatus: this.$getPageStatus?.(),
        //树节点
        permissionGroupTreeData: [],
        permissionGroupDataDict: {},
        //选择分组名称
        selectedGroupName: '',
        form: {
          id: 0,
          parent: 0,
          name: '',
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
    computed: {},
    created() {},
    methods: {
      //获取当前分组
      async handleQueryCurrentPermissionGroup() {
        this.pageStatus.showPageLoading('正在加载权限分组...')
        this.permissionGroupDataDict = {}
        var queryResult = await doQueryPermissionGroup({})
        if (queryResult && queryResult.success) {
          this.permissionGroupTreeData = this.convertToTreeData(
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
            vm.permissionGroupDataDict[item.id] = item
            return item.parent == parentId && item.id != this.form.id
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
      //检查权限分组名
      handleCheckGroupName(rule, value, callback) {
        if (!value) {
          return callback(new Error('请填写50个字符内的名称'))
        }
        var vm = this
        new Promise(async function (resolve, reject) {
          vm.pageStatus.disablePageHandle()
          const { data: allowUse } = await doCheckPermissionGroupName({
            name: vm.form.name,
            excludeId: vm.form.id,
          })
          if (allowUse) {
            callback()
          } else {
            callback('当前名称已存在')
          }
          vm.pageStatus.enablePageHandle()
        })
      },
      //显示编辑页面
      async showEditPermissionGroup(data) {
        if (!data) {
          this.title = '添加分组'
          this.selectedGroupName = ''
          this.form = Object.assign({}, {})
        } else {
          this.title = '编辑分组'
          this.form = Object.assign({}, data)
        }
        await this.handleQueryCurrentPermissionGroup()
        let groupData = this.permissionGroupDataDict[this.form.parent]
        if (groupData) {
          this.selectedGroupName = groupData.name
        }
        this.dialogFormVisible = true
      },
      //选择节点
      selectTreeNodeClick(data, node, el) {
        this.selectedGroupName = data.label
        this.form.parent = data.id
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
            this.pageStatus.showPageLoading('正在保存分组...')
            const { success, msg } = await doSavePermissionGroup(this.form)
            if (success) {
              this.$baseMessage('保存成功', 'success')
              this.$refs['form'].resetFields()
              this.dialogFormVisible = false
              this.$emit('save-success-callback')
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
