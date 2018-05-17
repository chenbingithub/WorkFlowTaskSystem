<style lang="less">
    @import '../../styles/common.less';
    @import './access.less';
</style>

<template>
    <div >
     <Card >
            <p slot="title">{{'部门管理'|l}}</p>
            
             <Row>
            <Col span="6">
                <Card style="min-height:400px;">
                    <p slot="title">
                        <Icon type="android-contact"></Icon>
                        当前用户
                    </p>
                    <Tree :data="organizations" @on-select-change="onselectchange"  ></Tree>
                </Card>
            </Col>
            <Col span="18" class="padding-left-10">
                <Card style="min-height:400px;">
                    <p slot="title">
                        <Icon type="android-contacts"></Icon>
                        不同部门菜单
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
                        <table class="hovertable" :model="detailsOrganization">
                            <tr ><td colspan="2" class="table_title">部门信息详情</td></tr>
                            <tr ><td class="td1">上级节点名称：</td><td class="td2">{{detailsOrganization.parentName}}</td></tr>
                            <tr ><td class="td1">编号：</td><td class="td2">{{detailsOrganization.no}}</td></tr>
                            <tr ><td class="td1">名称：</td><td class="td2">{{detailsOrganization.name}}</td></tr>
                            <tr ><td class="td1">编码：</td><td class="td2">{{detailsOrganization.code}}</td></tr>
                            <tr ><td class="td1">部门主管：</td><td class="td2">{{detailsOrganization.leader}}</td></tr>
                            <tr ><td class="td1">部门最高领导人：</td><td class="td2">{{detailsOrganization.header}}</td></tr>
                            <tr ><td class="td1">描述：</td><td class="td2">{{detailsOrganization.description}}</td></tr>
                            <tr ><td class="td1">默认角色：</td><td class="td2">{{detailsOrganization.roleNames}}</td></tr>
                            
                        </table>
                    </div>
                </Card>
            </Col>
        </Row>
    </Card>
       <Modal v-model="showModal" :title="L('添加')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                
                <Tabs>
                    <TabPane label="部门信息" icon="social-tux">
                        <Form ref="newOrganizationForm" :label-width="80"  :rules="newOrganizationRule" :model="editOrganization">
                            <FormItem :label="L('上级名称')" prop="parentName">
                                <Input v-model="editOrganization.parentName" disabled :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('编号')" prop="no">
                                <Input v-model="editOrganization.no" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('名称')" prop="name">
                                <Input v-model="editOrganization.name" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('编码')" prop="code">
                                <Input v-model="editOrganization.code" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('描述')" prop="description">
                                <Input v-model="editOrganization.description" type="textarea" :autosize="{minRows: 2,maxRows: 5}" placeholder="请输入..."></Input>
                            </FormItem>
                                              
                        </Form>
                    </TabPane>
                    <TabPane label="角色" icon="social-tux">
                        <Tree :data="roles" show-checkbox multiple ref="coroles"  ></Tree>
                    </TabPane>
                    <TabPane label="权限" icon="social-tux">
                        <Tree :data="pers" show-checkbox multiple ref="copers"  ></Tree>
                    </TabPane>
                </Tabs>
            </div>
            <div slot="footer">
                <Button @click="showModal=false">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
        <Modal v-model="showEditModal" :title="L('编辑')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                
                <Tabs>

                    <TabPane label="部门信息" icon="social-tux">
                       <Form ref="organizationForm" :label-width="80" :rules="organizationRule" :model="editOrganization">
                            <FormItem :label="L('上级名称')" prop="parentName">
                                <Input v-model="editOrganization.parentName"  disabled :maxlength="32"  :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('编号')" prop="no">
                                <Input v-model="editOrganization.no" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('名称')" prop="name">
                                <Input v-model="editOrganization.name" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('编码')" prop="code">
                                <Input v-model="editOrganization.code" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('描述')" prop="description">
                                <Input v-model="editOrganization.description" type="textarea" :autosize="{minRows: 2,maxRows: 5}" placeholder="请输入..."></Input>
                            </FormItem>
                                              
                        </Form>
                    </TabPane>
                    <TabPane label="角色" icon="social-tux">
                        <Tree :data="roles" show-checkbox multiple ref="uoroles"  ></Tree>
                    </TabPane>
                    <TabPane label="权限" icon="social-tux">
                        <Tree :data="pers" show-checkbox multiple ref="uopers"  ></Tree>
                    </TabPane>
                </Tabs>
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
            editOrganization:{},
            selectOrganization:{},
            detailsOrganization:{
                parentName:"",
                name:"",
                code:"",
                description:"",
                roleNames:"",
            },
            showModal:false,
            showEditModal:false,
            newOrganizationRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                no:[{required:true,message:'no is required',trigger: 'blur'}],
            },            
            organizationRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                no:[{required:true,message:'Code is required',trigger: 'blur'}],
            }
        }
    },
    
    methods: {
    create(){
        this.$store.dispatch({
                type:'organization/getorganzition',
                 data:"-1"
        });
        if(!!this.selectOrganization.data){
            this.editOrganization={
                parentId:this.selectOrganization.data.id,
                parentName:this.selectOrganization.data.name,

            };
            this.showModal=true;
        }else{
            this.editOrganization={};
            
            this.showModal=true;

        }
            
        },
        update(){

                if(!!this.selectOrganization.data){
                    this.editOrganization=this.selectOrganization.data;
                    this.$store.dispatch({
                        type:'organization/getorganzition',
                         data:this.editOrganization.id
                    });
                    this.showEditModal=true;
                }else{
                    this.$Message.warning('请选择编辑节点...');
                }
                
        },  
        deletes(){
            if(!!this.selectOrganization.data){
               
            }else{
             this.$Message.warning('请选择删除节点...');
                return;
            }
            this.$Modal.confirm({
                title:this.L(''),
                content:this.L('删除这个部门吗？'),
                okText:this.L('是'),
                cancelText:this.L('否'),
                onOk:async()=>{
                    await this.$store.dispatch({
                        type:'organization/delete',
                        data:this.selectOrganization.data
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
            if(!!this.editOrganization.id){
                this.$refs.organizationForm.validate(async (val)=>{
                    if(val){

                        var rc=[];
                        var pc=[];
                        var rcn=[];
                        var r=this.$refs.uoroles.getCheckedNodes();
                        var p=this.$refs.uopers.getCheckedNodes();
                        r.forEach(function(value,index,arr){
                            rc.push(value.id);
                            rcn.push(value.title);
                        });
                        
                        p.forEach(function(value,index,arr){
                            pc.push(value.id);
                        });
                        this.editOrganization.roleIds=rc;
                        this.editOrganization.persIds=pc;
                        this.editOrganization.roleNames=rcn.join(',');
                        await this.$store.dispatch({
                            type:'organization/update',
                            data:this.editOrganization
                        })
                        this.showEditModal=false;
                        this.$Message.success('保存成功！');
                        await this.getpage();
                    }
                })
                
            }else{
                this.$refs.newOrganizationForm.validate(async (val)=>{
                    if(val){
                        var rc=[];
                        var pc=[];
                        var rcn=[];
                        var r=this.$refs.coroles.getCheckedNodes();
                        var p=this.$refs.copers.getCheckedNodes();
                        r.forEach(function(value,index,arr){
                            rc.push(value.id);
                            rcn.push(value.title);
                        });
                        
                        p.forEach(function(value,index,arr){
                            pc.push(value.id);
                        });
                        this.editOrganization.roleIds=rc;
                        this.editOrganization.persIds=pc;
                        this.editOrganization.roleNames=rcn.join(',');
                        await this.$store.dispatch({
                            type:'organization/create',
                            data:this.editOrganization
                        })
                        this.showModal=false;
                        this.$Message.success('添加成功！');
                        await this.getpage();
                    }
                })
            }
            
        },
        onselectchange(data){
            this.selectOrganization=data.pop();
            this.detailsOrganization=this.selectOrganization.data;
        },
        async getpage(){
            await this.$store.dispatch({
                type:'organization/getAll'
            })
        }
    },
    computed:{
        
        organizations(){
            return this.$store.state.organization.organizations;
        },
        roles(){
            return this.$store.state.organization.roles;
        },
        pers(){
            return this.$store.state.organization.pers;
        },


        
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
