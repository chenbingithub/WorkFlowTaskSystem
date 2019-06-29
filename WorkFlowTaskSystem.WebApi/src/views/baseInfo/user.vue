<style lang="less">
    @import '../../styles/common.less';
    @import './access.less';
</style>

<template>
    <div >
     <Card >
            <p slot="title">{{'用户管理'|l}}</p>
            
             <Row>
            <Col span="5">
                <Card style="min-height:400px;">
                    <p slot="title">
                        组织部门
                    </p>
                    <Tree :data="organzitions" @on-select-change="onselectchange"  ></Tree>
                </Card>
            </Col>
            <Col span="19" class="padding-left-10">
            <Card style="min-height:400px;">
            <p slot="title">{{'用户信息'|l}}</p>
            <Dropdown slot="extra"  @on-click="handleClickActionsDropdown">
                <a href="javascript:void(0)">
                    {{'操作'|l}}
                    <Icon type="android-more-vertical"></Icon>
                </a>
                <DropdownMenu slot="list">
                    <DropdownItem name='Refresh'>{{'刷新' | l}}</DropdownItem>
                    <DropdownItem name='Create' v-if="persBtn.create" >{{'添加' | l}}</DropdownItem>
                </DropdownMenu>
            </Dropdown>
            <Input v-model="seachKey" @on-enter="seachKeyChange" >
                <span slot="prepend">关键字：</span>
                <Button slot="append" type="info"  @click="seachKeyChange">搜索</Button>
            </Input>
            <Table  :columns="columns" border :data="users"></Table>
            <Page :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage" show-sizer show-elevator show-total ></Page>
        </Card>
        <Modal v-model="showModal" :title="L('添加')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
               
                 <Tabs>
                    <TabPane label="用户信息" icon="ios-list">
                        <Form ref="newUserForm" :label-width="80" :rules="newUserRule" :model="editUser">
                            <FormItem :label="L('账号')" prop="userName">
                                <Input v-model="editUser.userName" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('邮箱')" prop="emailAddress">
                                <Input v-model="editUser.emailAddress" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                             <FormItem :label="L('姓名')" prop="name">
                                <Input v-model="editUser.name" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('电话')" prop="phoneNumber">
                                <Input v-model="editUser.phoneNumber" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                           
                            <FormItem :label="L('描述')" prop="description">
                                <Input v-model="editUser.description"></Input>
                            </FormItem>
                            <FormItem :label="L('是否启用')" prop="isActive">
                                <Checkbox  v-model="editUser.isActive" >启用</Checkbox >
                            </FormItem>               
                        </Form>
                    </TabPane>
                    <TabPane label="部门" icon="network" v-if="persBtn.setOrgan" >
                        <div style="max-height:320px;overflow-y:auto;">
                           <Tree :data="organ" show-checkbox multiple ref="corgan"  ></Tree>
                        </div>
                        
                    </TabPane>
                    <TabPane label="角色" icon="ios-people" v-if="persBtn.setRole">
                        <div style="max-height:320px;overflow-y:auto;">
                            <Tree :data="roles" show-checkbox multiple ref="croles"  ></Tree>
                        </div>
                        
                    </TabPane>
                    <TabPane label="权限" icon="gear-b" v-if="persBtn.setPers">
                        <div style="max-height:320px;overflow-y:auto;">
                           <Tree :data="pers" show-checkbox multiple ref="cpers"  ></Tree>
                        </div>
                        
                    </TabPane>
                </Tabs>
            </div>
            <div slot="footer">
                <Button @click="showModal=false">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
        <Modal v-model="showEditModal" :title="L('编辑')"  @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div> 
                <Tabs >
                    <TabPane label="用户信息" icon="ios-list">
                        <Form ref="userForm"  :label-width="80" :rules="userRule" :model="editUser">
                            <FormItem :label="L('账号')" prop="userName">
                                <Input v-model="editUser.userName" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('邮箱')" prop="emailAddress">
                                <Input v-model="editUser.emailAddress" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                             <FormItem :label="L('姓名')" prop="name">
                                <Input v-model="editUser.name" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('电话')" prop="phoneNumber">
                                <Input v-model="editUser.phoneNumber" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                           
                            <FormItem :label="L('描述')" prop="description">
                                <Input v-model="editUser.description"></Input>
                            </FormItem>
                            <FormItem :label="L('是否启用')" prop="isActive">
                                <Checkbox  v-model="editUser.isActive" >启用</Checkbox >
                            </FormItem>                                   
                        </Form>
                    </TabPane>
                    <TabPane label="部门" icon="network" v-if="persBtn.setOrgan" >
                        <div style="max-height:360px;overflow-y:auto;">
                            <Tree :data="organ" show-checkbox multiple ref="uorgan"  ></Tree>
                        </div>
                        
                    </TabPane>
                    <TabPane label="角色" icon="ios-people" v-if="persBtn.setRole" >
                        <div style="max-height:320px;overflow-y:auto;">
                            <Tree :data="roles" show-checkbox multiple ref="uroles"  ></Tree>
                        </div>
                    </TabPane>
                    <TabPane label="权限" icon="gear-b" v-if="persBtn.setPers">
                        <div style="max-height:320px;overflow-y:auto;">
                            <Tree :data="pers" show-checkbox multiple ref="upers"  ></Tree>
                        </div>        
                    </TabPane>
                </Tabs>
            </div>
            <div slot="footer">
                <Button @click="showEditModal=false">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
            </Col>
        </Row>
        </Card>
    </div>
</template>

<script>
import Cookies from 'js-cookie';
export default {
    name: 'access_index',
    data () {
        return {
            editUser:{},
            selectUser:{},
            persBtn:{
                create:abp.auth.isGranted('Pages.Users.Create'),
                update:abp.auth.isGranted('Pages.Users.Update'),
                delete:abp.auth.isGranted('Pages.Users.Delete'),
                setOrgan:abp.auth.isGranted('Pages.Users.SetOrgan'),
                setRole:abp.auth.isGranted('Pages.Users.SetRole'),
                setPers:abp.auth.isGranted('Pages.Users.SetPers'),
            },
            seachKey:"",
            showModal:false,
            showEditModal:false,
            newUserRule:{
                userName:[{required:true,message:'账号是必填项',trigger: 'blur'}],
                emailAddress:[
                    {required:true,message:'邮箱是必填项',trigger: 'blur'},
                    { type: 'email', message: '邮箱格式不正确', trigger: 'blur' }
                ],
                name:[{required:true,message:'姓名是必填项',trigger: 'blur'}]
            },            
            userRule:{
                 userName:[{required:true,message:'账号是必填项',trigger: 'blur'}],
                emailAddress:[
                    {required:true,message:'邮箱是必填项',trigger: 'blur'},
                    { type: 'email', message: '邮箱格式不正确', trigger: 'blur' }
                ],
                name:[{required:true,message:'姓名是必填项',trigger: 'blur'}]
            },
            columns:[
            
                    {
                        type: 'index',
                        width: 60,
                        align: 'center'
                    },
                    {
                        title:this.L('账号'),
                        key:'userName'
                    },
                    {
                        title:this.L('姓名'),
                        key:'name'
                    },
                    {
                        title:this.L('默认角色'),
                        key:'roleNames'
                    },
                    //{
                     ////   title:this.L('所在部门'),
                    //    key:'organizationUnitNames'
                    //},
                    {
                        title:this.L('是否启用'),
                        key:'isActive',
                        width:100,
                        render:(h,params)=>{
                            return h('strong', params.row.isActive?"已启用":"未启用");
                            
                        }
                    },
                    {
                        title: this.L('操作'),
                        key: 'action',
                        width:150,
                        render:(h,params)=>{
                            var btns=[];
                            if(this.persBtn.update){
                                var d=h('Button',{
                                            props:{
                                                type:'primary',
                                                size:'small'
                                            },
                                            style:{
                                                marginRight:'5px'
                                            },
                                            on:{
                                                click:()=>{
                                                    this.editUser=this.users[params.index];
                                                    this.$store.dispatch({
                                                        type:'user/getuser',
                                                        data:this.editUser.id
                                                    })
                                                    this.showEditModal=true;
                                                }
                                            }
                                        },this.L('编辑'));
                                btns.push(d);
                            }
                           if(this.persBtn.delete){
                                   var d1=h('Button',{
                                            props:{
                                                type:'error',
                                                size:'small'
                                            },
                                            on:{
                                                click:async()=>{
                                                    this.$Modal.confirm({
                                                        title:this.L(''),
                                                        content:this.L('删除这个用户吗？'),
                                                        okText:this.L('是'),
                                                        cancelText:this.L('否'),
                                                        onOk:async()=>{
                                                            await this.$store.dispatch({
                                                                type:'user/delete',
                                                                data:this.users[params.index]
                                                            })
                                                            this.$Message.success('删除成功！');
                                                            await this.getpage();
                                                        }
                                                    })
                                                }
                                        }
                                    },this.L('删除'));
                                   btns.push(d1); 
                            }
                            return h('div',btns);
                        }
                    }
            ]
        }
    },
    
    methods: {
        create(){
            this.editUser={isActive:true};
            this.$store.dispatch({
                            type:'user/getuser',
                            data:"-1"
                        })
            this.showModal=true;
        },
        async save(){
            if(!!this.editUser.id){
                this.$refs.userForm.validate(async (val)=>{
                    if(val){
                    var rc=[];
                    var oc=[];
                    var pc=[];
                    var rcn=[];
                    var ocn=[];
                    var r=this.$refs.uroles.getCheckedNodes();
                    var o=this.$refs.uorgan.getCheckedNodes();
                    var p=this.$refs.upers.getCheckedNodes();
                    r.forEach(function(value,index,arr){
                        rc.push(value.id);
                        rcn.push(value.title);
                    });
                    o.forEach(function(value,index,arr){
                        oc.push(value.id);
                        ocn.push(value.title);
                    });
                    p.forEach(function(value,index,arr){
                        pc.push(value.id);
                    });
                    this.editUser.roleIds=rc;
                    this.editUser.organIds=oc;
                    this.editUser.persIds=pc;
                    this.editUser.roleNames=rcn.join(',');
                    this.editUser.organizationUnitNames=ocn.join(',');
                        await this.$store.dispatch({
                            type:'user/update',
                            data:this.editUser
                        })
                        this.showEditModal=false;
                        this.$Message.success('保存成功！');
                        await this.getpage();
                    }
                })
                
            }else{
                this.$refs.newUserForm.validate(async (val)=>{
                    if(val){
                    var rc=[];
                    var oc=[];
                    var pc=[];
                    var rcn=[];
                    var ocn=[];
                    var r=this.$refs.croles.getCheckedNodes();
                    var o=this.$refs.corgan.getCheckedNodes();
                    var p=this.$refs.cpers.getCheckedNodes();
                    r.forEach(function(value,index,arr){
                        rc.push(value.id);
                        rcn.push(value.title);
                    });
                    o.forEach(function(value,index,arr){
                        oc.push(value.id);
                        ocn.push(value.title);
                    });
                    p.forEach(function(value,index,arr){
                        pc.push(value.id);
                    });
                    this.editUser.roleIds=rc;
                    this.editUser.organIds=oc;
                    this.editUser.persIds=pc;
                    this.editUser.roleNames=rcn.join(',');
                    this.editUser.organizationUnitNames=ocn.join(',');
                        await this.$store.dispatch({
                            type:'user/create',
                            data:this.editUser
                        })
                        this.showModal=false;
                        this.$Message.success('添加成功！');
                        await this.getpage();
                    }
                })
            }
            
        },
        pageChange(page){
            this.$store.commit('user/setCurrentPage',page);
            this.getpage();
        },
        pagesizeChange(pagesize){
            this.$store.commit('user/setPageSize',pagesize);
            this.getpage();
        },
        seachKeyChange(){
            this.$store.commit('user/setSeachKey',this.seachKey);
            this.getpage();
        },
        async getpage(){
            await this.$store.dispatch({
                type:'user/getAll'
            });
             
        },
        
        handleClickActionsDropdown(name){
            if(name==='Create'){
                this.create();
            }else if(name==='Refresh'){
                this.getpage();
                this.$Message.success('刷新成功！');

            }
        },
        onselectchange(data){
            this.selectUser=data.pop();
            this.$store.commit('user/setOrganzitionId',this.selectUser.data.id);
            this.getpage();
        },
        async getAllorganiztion(){
             await this.$store.dispatch({
                type:'organization/getAll'
            })
        }
       
    },
    computed:{
        
        organzitions(){
            return this.$store.state.organization.organizations;
        },
        users(){
            return this.$store.state.user.users;
        },
        
        totalCount(){
            return this.$store.state.user.totalCount;
        },
        currentPage(){
            return this.$store.state.user.currentPage;
        },
        pageSize(){
            return this.$store.state.user.pageSize;
        },
        roles(){
            return this.$store.state.user.roles;
        },
        pers(){
            return this.$store.state.user.pers;
        },
        organ(){
            return this.$store.state.user.organ;
        }

        
    },
    async created(){
        this.getpage();
        this.getAllorganiztion();
        
    }
};
</script>

