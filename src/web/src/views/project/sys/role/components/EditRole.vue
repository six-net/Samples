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
      <el-form-item label="名称" prop="name">
        <el-input v-model.trim="form.name" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="状态" prop="status">
        <el-select v-model="form.status" placeholder="请选择状态">
          <el-option
            v-for="item in getRoleStatusDict"
            :key="item.key"
            :label="item.value"
            :value="parseInt(item.key)"
          ></el-option>
        </el-select>
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
  import { createNamespacedHelpers } from 'vuex'
  import { doSaveRole, doCheckRoleName } from '@/api/sys/role'
  const { mapGetters } = createNamespacedHelpers('sys/role')
  export default {
    name: 'EditRole',
    data() {
      return {
        pageStatus: this.$getPageStatus?.(),
        form: {
          id: 0,
          name: '',
          status: 0,
          remark: '',
        },
        rules: {
          name: [
            {
              required: true,
              min: 1,
              max: 20,
              trigger: 'blur',
              message: '请输入20个字符内的名称',
            },
            {
              validator: this.handleCheckRoleName,
              trigger: 'blur',
            },
          ],
          status: [
            { required: true, trigger: 'change', message: '请选择状态' },
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
      ...mapGetters(['getRoleStatusDict', 'getRoleDefaultStatus']),
    },
    created() {},
    methods: {
      //检查角色名
      handleCheckRoleName(rule, value, callback) {
        if (!value) {
          return callback(new Error('请填写50个字符内的名称'))
        }
        var vm = this
        new Promise(async function (resolve, reject) {
          vm.pageStatus.disablePageHandle()
          const { data: allowUse } = await doCheckRoleName({
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
      showEdit(row) {
        if (!row) {
          this.title = '添加角色'
          this.form = this.$options.data().form
          this.form.status = this.getRoleDefaultStatus
        } else {
          this.title = '编辑角色'
          this.form = Object.assign({}, row)
        }
        this.dialogFormVisible = true
      },
      close() {
        this.$refs['form'].resetFields()
        this.form = this.$options.data().form
        this.dialogFormVisible = false
      },
      save() {
        this.$refs['form'].validate(async (valid) => {
          if (valid) {
            this.pageStatus.showPageLoading('正在保存角色...')
            const { success, msg } = await doSaveRole(this.form)
            if (success) {
              this.$baseMessage('保存成功', 'success')
              this.$refs['form'].resetFields()
              this.dialogFormVisible = false
              this.$emit('fetch-data')
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
