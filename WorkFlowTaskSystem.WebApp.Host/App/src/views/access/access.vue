<style lang="less">
    @import '../../styles/common.less';
    @import './access.less';
</style>

<template>
    <div >
     <Card >
            <p slot="title">{{'权限管理'|l}}</p>
            
             <Row>
            <Col span="6">
                <Card style="min-height:400px;">
                    <p slot="title">
                        <Icon type="android-contact"></Icon>
                        当前用户
                    </p>
                   <Tree :data="permissions" @on-select-change="onselectchange"  ></Tree>
                </Card>
            </Col>
            <Col span="18" class="padding-left-10">
                <Card style="min-height:400px;">
                    <p slot="title">
                        <Icon type="android-contacts"></Icon>
                        不同权限菜单
                    </p>
                    <Dropdown slot="extra">
                        <ButtonGroup >
                        <Button type="ghost" icon="social-facebook" @click="refresh">刷新</Button>
                        <Button type="ghost" icon="social-twitter" @click="create" >添加</Button>
                        <Button type="ghost" icon="social-googleplus" @click="update">编辑</Button>
                        <Button type="ghost" icon="social-tumblr" @click="deletes">删除</Button>
                        </ButtonGroup>
                    </Dropdown>
                    
                    <div class="padding10">
                        <table class="hovertable" :model="detailsPermission">
                            <tr ><td colspan="2" class="table_title">权限信息详情</td></tr>
                            <tr ><td class="td1">上级节点名称：</td><td class="td2">{{detailsPermission.parentName}}</td></tr>
                            <tr ><td class="td1">名称：</td><td class="td2">{{detailsPermission.name}}</td></tr>
                            <tr ><td class="td1">编码：</td><td class="td2">{{detailsPermission.code}}</td></tr>
                            <tr ><td class="td1">描述：</td><td class="td2">{{detailsPermission.description}}</td></tr>
                            
                        </table>
                    </div>
                </Card>
            </Col>
        </Row>
    </Card>
       <Modal v-model="showModal" :title="L('添加')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                <Form ref="newPermissionForm" label-position="top" :rules="newPermissionRule" :model="editPermission">
                    <FormItem :label="L('上级名称')" prop="parentName">
                        <Input v-model="editPermission.parentName" disabled :maxlength="32" :minlength="2"></Input>
                    </FormItem>
                    <FormItem :label="L('名称')" prop="name">
                        <Input v-model="editPermission.name" :maxlength="32" :minlength="2"></Input>
                    </FormItem>
                    <FormItem :label="L('编码')" prop="code">
                        <Input v-model="editPermission.code" :maxlength="32" :minlength="2"></Input>
                    </FormItem>
                    <FormItem :label="L('描述')" prop="description">
                        <Input v-model="editPermission.description" type="textarea" :autosize="{minRows: 2,maxRows: 5}" placeholder="请输入..."></Input>
                    </FormItem>
                                      
                </Form>
            </div>
            <div slot="footer">
                <Button @click="showModal=false">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
        <Modal v-model="showEditModal" :title="L('编辑')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                <Form ref="permissionForm" label-position="top" :rules="permissionRule" :model="editPermission">
                    <FormItem :label="L('上级名称')" prop="parentName">
                        <Input v-model="editPermission.parentName"  disabled :maxlength="32"  :minlength="2"></Input>
                    </FormItem>
                    <FormItem :label="L('名称')" prop="name">
                        <Input v-model="editPermission.name" :maxlength="32" :minlength="2"></Input>
                    </FormItem>
                    <FormItem :label="L('编码')" prop="code">
                        <Input v-model="editPermission.code" :maxlength="32" :minlength="2"></Input>
                    </FormItem>
                    <FormItem :label="L('描述')" prop="description">
                        <Input v-model="editPermission.description" type="textarea" :autosize="{minRows: 2,maxRows: 5}" placeholder="请输入..."></Input>
                    </FormItem>
                                      
                </Form>
            </div>
            <div slot="footer">
                <Button @click="showEditModal=false">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
    </div>
</template>

<script>
import Cookies from 'js-cookie';
export default {
    name: 'access_index',
    data () {
        return {
            editPermission:{},
            selectPermission:{},
            detailsPermission:{
                parentName:"",
                name:"",
                code:"",
                description:"",
            },
            showModal:false,
            showEditModal:false,
            newPermissionRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                code:[{required:true,message:'Code is required',trigger: 'blur'}],
            },            
            permissionRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                code:[{required:true,message:'Code is required',trigger: 'blur'}],
            }
             

        }
    },
    
    methods: {
   
    onclickztree(event, treeId, treeNode) {
        alert(treeNode.tId + ", " + treeNode.name);
    },
    create(){
        if(!!this.selectPermission.data){
            this.editPermission={
                isActive:true,
                parentId:this.selectPermission.data.id,
                parentName:this.selectPermission.data.name,

            };
            this.showModal=true;
        }else{
            this.editPermission={isActive:true};
            this.showModal=true;

        }
            
        },
        update(){

                if(!!this.selectPermission.data){
                    this.editPermission=this.selectPermission.data;
                    this.showEditModal=true;
                }else{
                    this.$Message.warning('请选择编辑节点...');
                }
                
        },  
        deletes(){
            if(!!this.selectPermission.data){
               
            }else{
             this.$Message.warning('请选择删除节点...');
                return;
            }
            this.$Modal.confirm({
                title:this.L(''),
                content:this.L('删除这个权限吗？'),
                okText:this.L('是'),
                cancelText:this.L('否'),
                onOk:async()=>{
                    await this.$store.dispatch({
                        type:'permission/delete',
                        data:this.selectPermission.data
                    })
                    this.$Message.success('删除成功！');
                    this.getpage();
                }
            })
        }, 
        refresh(){
            this.getpage();
            this.$Message.success('刷新成功！');
        },              
        async save(){
            if(!!this.editPermission.id){
                this.$refs.permissionForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'permission/update',
                            data:this.editPermission
                        })
                        this.showEditModal=false;
                        this.$Message.success('保存成功！');
                        await this.getpage();
                    }
                })
                
            }else{
                this.$refs.newPermissionForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'permission/create',
                            data:this.editPermission
                        })
                        this.showModal=false;
                        this.$Message.success('添加成功！');
                        await this.getpage();
                    }
                })
            }
            
        },
        onselectchange(data){
            this.selectPermission=data.pop();
            this.detailsPermission=this.selectPermission.data;
            console.log(data);
        },
        async getpage(){
            await this.$store.dispatch({
                type:'permission/getAllTree'
            });
            
        }
         
    },
    computed:{
        
        permissions(){
            return this.$store.state.permission.permissions;
        }
      


        
    },
    async created(){
        this.getpage();
       
    }
};
</script>

<style>
        .form_table {
    width: 100%;
    table-layout: fixed;
}

    .form_table th {
        border: 1px solid #CCCCCC;
        padding: 5px;
        color:gray;
        text-align: right;
        /*background-color: #E0ECFF;*/
        width: 180px;
    }

    .form_table td {
        border: 1px solid #CCCCCC;
        padding: 6px 0 5px 10px;
        text-align: left;
        color: #717171;
        line-height: 200%;
    }

    .bg-light {
  color: #58666e;
  background-color: #edf1f2;
}
    .wrapper-md {
  padding: 20px;
}

.bg-light.lter,
.bg-light .lter {
  background-color: #f6f8f8;
}


.m-n {
  margin: 0 !important;
}


.font-thin {
  font-weight: 300;
}


.form_table1 {
    width: 100%;
    table-layout: fixed;
}

.padding10 {
    padding: 10px !important;
}
.nobordertable ,.hovertable{
            margin-right: auto;
            margin-left: auto;
            width:99%;
            
        }
 .nobordertable,.nobordertable tr {  
             font-family: "宋体";
            font-size: 14px;
            color: #000000;
            border-collapse: collapse; 
     }  
 .nobordertable td {
            font-family: "宋体";
            font-size: 14px;
            color: #045795;
            padding-top:10px;
 }

  .nobordertable .td1 {
    text-align: right;
    font-size: 12px;
    color: #045795;
    font-weight: bold;
    width: 20%;
    }
    .nobordertable .td2 {
    text-align: left;
    font-size: 12px;
    color: #222;
    padding-left: 10px;
    }
     .hovertable,.hovertable tr, .hovertable td {  
             font-family: "宋体";
            font-size: 14px;
            color: #000000;
            line-height: 30px;
            border: 1px solid #ABCFF8;
            border-collapse: collapse; 
     }  
     .hovertable .td1 {
    text-align: right;
    font-size: 12px;
    color: #045795;
    font-weight: bold;
    width: 20%;
    }
    .hovertable .td2 {
    text-align: left;
    font-size: 12px;
    color: #222;
    padding-left: 10px;
    }
    .table_title {
    font-weight: bold;
    height: 32px;
    line-height: 32px;
    text-align: center;
    padding-left: 10px;
    color: #045795;
    background-color: #e9f4fd;
}
      
</style>
