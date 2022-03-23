<template>
  <el-select
    ref="selectInput"
    :value="selectedText"
    class="vab-tree-select"
    clearable
    popper-class="select-tree-popper"
    value-key="id"
    @clear="handleClearSelectedMenu"
  >
    <el-option :value="selectedValue">
      <el-tree
        id="selectTree"
        ref="selectTree"
        :current-node-key="selectedValue"
        :data="treeData"
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
</template>

<script>
  export default {
    name: 'SelectMenu',
    props: {
      allData: {
        type: Array,
        default: () => [],
      },
      selectedValue: {
        type: String,
        default: '',
      },
    },
    data() {
      return {
        menuDataDict: {},
      }
    },
    computed: {
      treeData() {
        return this.convertToTreeData(this.allData, '0')
      },
      selectedText() {
        return this.menuDataDict[this.selectedValue]?.title
      },
    },
    created() {},
    methods: {
      //转换成树数据
      convertToTreeData(datas, parentId) {
        let treeDatas = []
        if (datas && datas.length > 0) {
          var topDatas = datas.filter((item) => {
            this.menuDataDict[item.id] = item
            return item.parent == parentId
          })
          topDatas.map((item) => {
            let nodeData = {
              id: item.id,
              label: item.title,
            }
            let childDatas = this.convertToTreeData(datas, item.id)
            nodeData.children = childDatas
            treeDatas.push(nodeData)
          })
        }
        return treeDatas
      },
      //选择节点
      selectTreeNodeClick(data, node, el) {
        this.$emit('selected-data', data)
        this.$refs.selectInput.blur()
      },
      //清除选择
      handleClearSelectedMenu() {
        this.$emit('selected-data', null)
        this.$refs.selectInput.blur()
      },
    },
  }
</script>

<style>
  .el-input__inner {
    padding-right: 0px !important;
  }
</style>
