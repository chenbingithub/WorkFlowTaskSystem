<template>  
    <div >  
         <ul id="treeDemo" class="ztree"></ul>  
    </div>  
</template>  
  
<script>  
import  'jquery';
import  'ztree';
import Vue from 'vue';
    export default {  
      name: 'zTree',  
      components:{  
  
      },  
      props: {
          data:{
                type: Array,
                default () {
                    return [];
                }
          },
          settingdata:{
              type:Object,
          }
        },
      data:function(){  
        return{  
            setting:{  
                data: this.settingdata,  
                callback: {  
                    onClick: this.onClick,  
                }  
            }
        }  
      },  
      methods:{  
        init: function(){  
            $.fn.zTree.init($("#treeDemo"), this.setting, this.data);  
        }
      },  
      mounted(){  
        $.fn.zTree.init($("#treeDemo"), this.setting, this.data).expandAll(true);  
      }  
    }  
</script>  
<style>  
    import  'ztree/css/metroStyle/metroStyle.css';
</style>  