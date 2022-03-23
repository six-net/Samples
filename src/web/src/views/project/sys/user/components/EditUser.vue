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
      <el-form-item label="登录名" prop="userName">
        <el-input
          v-model.trim="form.userName"
          :disabled="isEdit"
          autocomplete="off"
        ></el-input>
      </el-form-item>
      <el-form-item label="姓名" prop="realName">
        <el-input v-model.trim="form.realName" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="状态" prop="status">
        <el-select
          v-model="form.status"
          :disabled="form.superUser"
          placeholder="请选择状态"
        >
          <el-option
            v-for="item in getUserStatusDict"
            :key="item.key"
            :label="item.value"
            :value="parseInt(item.key)"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item v-if="!isEdit" label="密码" prop="password">
        <el-input
          v-model.trim="form.password"
          autocomplete="off"
          show-password
        ></el-input>
      </el-form-item>
      <el-form-item label="手机" prop="mobile">
        <el-input v-model.trim="form.mobile" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="邮箱" prop="email">
        <el-input v-model.trim="form.email" autocomplete="off"></el-input>
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button icon="el-icon-close" :disabled="false" @click="close">
        取 消
      </el-button>
      <el-button
        icon="el-icon-receiving"
        type="primary"
        :loading="false"
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
  import { doSaveUser, doCheckUserName } from '@/api/sys/user'
  const { mapGetters: mapUserGetters } = createNamespacedHelpers('sys/user')
  export default {
    name: 'EditUser',
    data() {
      return {
        isEdit: false,
        pageStatus: this.$getPageStatus?.(),
        form: {
          id: 0,
          userName: '',
          realName: '',
          status: 0,
          password: '',
          mobile: '',
          email: '',
          superUser: false,
        },
        rules: {
          userName: [
            {
              required: true,
              min: 1,
              max: 30,
              validator: this.handleCheckUserName,
              trigger: 'blur',
            },
          ],
          realName: [
            {
              required: true,
              min: 1,
              max: 20,
              trigger: 'blur',
              message: '请输入20个字符内的姓名',
            },
          ],
          status: [
            { required: true, trigger: 'change', message: '请选择状态' },
          ],
          password: [
            {
              required: true,
              min: 1,
              max: 30,
              trigger: 'blur',
              message: '请输入30个字符内的密码',
            },
          ],
          mobile: [
            {
              type: 'string',
              pattern: /^[1][0-9]{10}$/,
              trigger: 'blur',
              message: '请填写正确的手机号码',
            },
          ],
          email: [
            {
              type: 'email',
              trigger: 'blur',
              message: '请填写正确的邮箱',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
      }
    },
    computed: {
      ...mapUserGetters(['getUserStatusDict', 'getUserDefaultStatus']),
    },
    created() {},
    methods: {
      //检查登录名
      handleCheckUserName(rule, value, callback) {
        if (this.isEdit) {
          return callback()
        }
        if (!value || value.length > 30) {
          return callback(new Error('请填写30个字符内的登录名'))
        }
        let vm = this
        new Promise(async function (resolve, reject) {
          vm.pageStatus.disablePageHandle()
          const { data: allowUse } = await doCheckUserName({
            userName: vm.form.userName,
            excludeId: vm.form.id,
          })
          if (allowUse) {
            callback()
          } else {
            callback('当前登录名已存在')
          }
          vm.pageStatus.enablePageHandle()
        })
      },
      //显示编辑页面
      showEdit(row) {
        if (!row) {
          this.title = '添加用户'
          this.form = this.$options.data().form
          this.form.status = this.getUserDefaultStatus
        } else {
          this.title = '编辑用户'
          this.isEdit = true
          this.form = Object.assign({}, row)
        }
        this.dialogFormVisible = true
      },
      close() {
        this.$refs['form'].resetFields()
        this.form = this.$options.data().form
        this.isEdit = false
        this.dialogFormVisible = false
      },
      save() {
        this.$refs['form'].validate(async (valid) => {
          if (valid) {
            const { success } = await doSaveUser(this.form)
            if (success) {
              this.$baseMessage('保存成功', 'success')
              this.$refs['form'].resetFields()
              this.dialogFormVisible = false
              this.$emit('fetch-data')
              this.form = this.$options.data().form
            } else {
              return false
            }
          } else {
            return false
          }
        })
      },
    },
  }
</script>
