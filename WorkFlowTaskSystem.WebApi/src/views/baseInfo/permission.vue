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
                        权限菜单
                    </p>
                   <Tree :data="permissions" @on-select-change="onselectchange"  ></Tree>
                </Card>
            </Col>
            <Col span="18" class="padding-left-10">
                <Card style="min-height:400px;">
                    <p slot="title">
                        详情
                    </p>
                   
                    <ButtonGroup slot="extra" >
                        <Button type="ghost" icon="refresh" @click="refresh">刷新</Button>
                        <Button type="ghost" icon="plus" v-if="persBtn.create" 
                        @click="create" >添加</Button>
                        <Button type="ghost" icon="edit" v-if="persBtn.update" @click="update">编辑</Button>
                        <Button type="ghost" icon="trash-a" v-if="persBtn.delete" @click="deletes">删除</Button>
                        </ButtonGroup>
                    <div class="padding10" v-if="showDetails">
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
            showDetails:false,
            persBtn:{
                create:abp.auth.isGranted('Pages.Permissions.Create'),
                update:abp.auth.isGranted('Pages.Permissions.Update'),
                delete:abp.auth.isGranted('Pages.Permissions.Delete'),
            },
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
            this.showDetails=true;
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


