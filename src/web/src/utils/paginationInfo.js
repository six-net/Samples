class PaginationInfoManager {
  small = false //是否使用小型分页样式
  background = false //是否为分页按钮添加背景色
  pageSize = 20 //每页显示条目个数
  total = 0 //总条目数
  pageCount = 0 //总页数
  pagerCount = 7 //页码按钮的数量
  currentPage = 1 //当前页数
  layout = 'total, sizes, prev, pager, next, jumper' //组件布局
  pageSizes = [10, 20, 30, 40, 50, 100] //每页显示个数选择器的选项设置
  popperClass = undefined //每页显示个数选择器的下拉框类名
  prevText = undefined //替代图标显示的上一页文字
  nextText = undefined //替代图标显示的下一页文字
  disabled = false //是否禁用
  hideOnSinglePage = undefined //只有一页时是否隐藏
  // sizeChange = this.handleSizeChange //每一页条数修改的时候触发,每页条数
  // currentChange = this.handleCurrentPageChange //currentPage 改变时会触发
  prevClick = undefined //用户点击上一页按钮改变当前页后触发
  nextClick = undefined //用户点击下一页按钮改变当前页后触发
  handleQueryData = undefined //查询数据方法

  //当前页修改触发
  handleCurrentPageChange(val) {
    this.currentPage = val
    this.handleQueryData?.()
  }

  //单页数据条数调整
  handleSizeChange(val) {
    this.pageSize = val
    this.handleQueryData?.()
  }

  //获取分页筛选
  getQueryForm(form) {
    let pageQueryForm = {
      page: this.currentPage,
      pageSize: this.pageSize,
    }
    if (form) {
      pageQueryForm = Object.assign(pageQueryForm, form)
    }
    return pageQueryForm
  }

  //初始化分页
  initPage() {
    this.currentPage = 1
  }

  //设置总条数
  setTotal(totalNum) {
    this.total = parseInt(totalNum)
  }
}

//获取分页信息
export function getPaginationInfo(options) {
  let pagination = new PaginationInfoManager()
  if (options) {
    pagination = Object.assign(pagination, options)
  }
  return pagination
}

//获取简易分页配置信息
export function getLwtPaginationInfo(options) {
  let pagination = getPaginationInfo(options)
  if (!options || !options.layout) {
    pagination.layout = 'total, sizes, prev, pager, next'
  }
  return pagination
}
