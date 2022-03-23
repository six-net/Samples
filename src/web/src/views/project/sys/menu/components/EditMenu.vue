<template>
  <el-dialog
    :title="pageTitle"
    :visible.sync="dialogFormVisible"
    :close-on-click-modal="false"
    :close-on-press-escape="false"
    width="650px"
    @close="close"
  >
    <el-form
      ref="menu-form"
      :model="form"
      :inline="true"
      :rules="rules"
      label-width="80px"
      style="padding: 0px"
    >
      <el-form-item label="类型" prop="usage">
        <el-select v-model="form.usage" placeholder="请选择类型">
          <el-option
            v-for="item in getMenuUsageDict"
            :key="item.key"
            :label="item.value"
            :value="parseInt(item.key)"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="上级路由" prop="parent">
        <el-col :span="24">
          <select-menu
            :all-data="allMenuData"
            :selected-value="form.parent.toString()"
            @selected-data="handleSelectParent"
          ></select-menu>
        </el-col>
      </el-form-item>
      <el-form-item label="标题" prop="title">
        <el-input v-model.trim="form.title"></el-input>
      </el-form-item>
      <el-form-item label="路由名称" prop="name">
        <el-input v-model.trim="form.name"></el-input>
      </el-form-item>
      <el-form-item label="路径" prop="path">
        <el-input v-model.trim="form.path"></el-input>
      </el-form-item>
      <el-form-item label="页面组件" prop="component">
        <el-input v-model.trim="form.component"></el-input>
      </el-form-item>
      <el-form-item label="跳转类型" prop="redirect">
        <el-input v-model.trim="form.redirect"></el-input>
      </el-form-item>
      <el-form-item label="路由图标" prop="remixIcon">
        <el-input v-model.trim="form.remixIcon"></el-input>
      </el-form-item>
      <el-form-item label="状态" prop="status">
        <el-select v-model="form.status" placeholder="请选择状态">
          <el-option
            v-for="item in getMenuStatusDict"
            :key="item.key"
            :label="item.value"
            :value="parseInt(item.key)"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="提示内容" prop="badge">
        <el-input v-model.trim="form.badge"></el-input>
      </el-form-item>
      <el-form-item label="菜单图标" prop="icon">
        <el-input v-model.trim="form.icon"></el-input>
      </el-form-item>
      <el-form-item label="激活菜单" prop="activeMenu">
        <el-col :span="24">
          <select-menu
            :all-data="allMenuData"
            :selected-value="form.activeMenu.toString()"
            @selected-data="handleSelectActiveMenu"
          ></select-menu>
        </el-col>
      </el-form-item>
      <el-form-item label="排序号" prop="sequence">
        <el-input v-model.trim="form.sequence"></el-input>
      </el-form-item>
      <el-form-item label="路径导航" prop="breadCrumb">
        <el-switch
          v-model="form.breadCrumb"
          active-color="#13ce66"
          inactive-color="#ff4949"
        ></el-switch>
      </el-form-item>
      <el-form-item label="禁用缓存" prop="noKeepAlive">
        <el-switch
          v-model="form.noKeepAlive"
          active-color="#13ce66"
          inactive-color="#ff4949"
        ></el-switch>
      </el-form-item>
      <el-form-item label="始终显示" prop="alwaysShow">
        <el-switch
          v-model="form.alwaysShow"
          active-color="#13ce66"
          inactive-color="#ff4949"
        ></el-switch>
      </el-form-item>
      <el-form-item label="固定" prop="affix">
        <el-switch
          v-model="form.affix"
          active-color="#13ce66"
          inactive-color="#ff4949"
        ></el-switch>
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="handleSaveMenu">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
  import { createNamespacedHelpers } from 'vuex'
  import { doQueryMenu, doSaveMenu } from '@/api/sys/menu'
  import SelectMenu from './SelectMenu'
  const { mapGetters: mapMenuGetters, mapActions: mapMenuActions } =
    createNamespacedHelpers('sys/menu')
  export default {
    name: 'EditMenu',
    components: { SelectMenu },
    data() {
      return {
        form: {
          parent: '0',
          name: '',
          path: '',
          component: '',
          redirect: 'noRedirect',
          icon: '',
          remixIcon: '',
          noKeepAlive: false,
          title: '',
          status: 0,
          alwaysShow: false,
          affix: false,
          badge: '',
          breadCrumb: true,
          activeMenu: 0,
          sequence: 0,
          usage: 0,
        },
        rules: {
          title: [{ required: true, trigger: 'blur', message: '请输入标题' }],
        },
        pageTitle: '',
        dialogFormVisible: false,
        //菜单数据
        allMenuData: [],
      }
    },
    computed: {
      ...mapMenuGetters([
        'getMenuStatusDict',
        'getMenuDefaultStatus',
        'getMenuUsageDict',
        'getMenuDefaultUsage',
      ]),
    },
    async created() {
      await this.initMenuConfig()
    },
    methods: {
      ...mapMenuActions(['initMenuConfig']),
      //查询当前信息
      async handleQueryCurrentMenu() {
        const { success, data } = await doQueryMenu({})
        if (success) {
          this.allMenuData = data
        }
      },
      //显示编辑页面
      async showEdit(row) {
        await this.initMenuConfig()
        await this.handleQueryCurrentMenu()
        if (!row) {
          this.pageTitle = '添加菜单'
          this.form = this.$options.data().form
          this.form.status = this.getMenuDefaultStatus
          this.form.usage = this.getMenuDefaultUsage
        } else {
          this.pageTitle = '编辑菜单'
          this.form = Object.assign({}, row)
        }
        this.dialogFormVisible = true
      },
      //选择上级
      handleSelectParent($event) {
        if ($event?.id) {
          this.form.parent = $event.id
        } else {
          this.form.parent = '0'
        }
      },
      //选择激活菜单
      handleSelectActiveMenu($event) {
        if ($event?.id) {
          this.form.activeMenu = $event.id
        } else {
          this.form.activeMenu = '0'
        }
      },
      //关闭页面
      close() {
        this.$refs['menu-form'].resetFields()
        this.form = this.$options.data().form
        this.dialogFormVisible = false
      },
      //保存菜单数据
      async handleSaveMenu() {
        this.$refs['menu-form'].validate(async (valid) => {
          if (valid) {
            const { msg, success } = await doSaveMenu(this.form)
            if (success) {
              this.$baseMessage('保存成功', 'success')
              this.$emit('fetch-data')
              this.close()
            } else {
              this.$baseMessage(msg, 'error')
            }
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
</style>
