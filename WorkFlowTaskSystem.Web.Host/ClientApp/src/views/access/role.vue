<template>
    <div>
        <Card>
            <p slot="title">{{'角色管理'|l}}</p>
            <Dropdown slot="extra"  @on-click="handleClickActionsDropdown">
                <a href="javascript:void(0)">
                    {{'操作'|l}}
                    <Icon type="android-more-vertical"></Icon>
                </a>
                <DropdownMenu slot="list">
                    <DropdownItem name='Refresh'>{{'刷新' | l}}</DropdownItem>
                    <DropdownItem name='Create'>{{'添加' | l}}</DropdownItem>
                </DropdownMenu>
            </Dropdown>
            <Table  :columns="columns" border :data="roles"></Table>
            <Page :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
        </Card>
        <Modal v-model="showModal" :title="L('添加')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                
                <Tabs>
                 <TabPane label="角色信息" icon="social-tux">
                        <Form ref="newRoleForm" label-position="top" :rules="newRoleRule" :model="editRole">
                            <FormItem :label="L('角色名称')" prop="name">
                                <Input v-model="editRole.name" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('角色编码')" prop="code">
                                <Input v-model="editRole.code" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('描述')" prop="description">
                                <Input v-model="editRole.description"></Input>
                            </FormItem>
                                              
                        </Form>
                    </TabPane>
                    <TabPane label="权限" icon="social-tux">
                        <Tree :data="pers" show-checkbox multiple ref="rcpers"  ></Tree>
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
                 <TabPane label="角色信息" icon="social-tux">
                        <Form ref="roleForm" label-position="top" :rules="roleRule" :model="editRole">
                            <FormItem :label="L('角色名称')" prop="name">
                                <Input v-model="editRole.name" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('角色编码')" prop="code">
                                <Input v-model="editRole.code" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('描述')" prop="description">
                                <Input v-model="editRole.description"></Input>
                            </FormItem>
                                              
                        </Form>
                    </TabPane>
                    <TabPane label="权限" icon="social-tux">
                        <Tree :data="pers" show-checkbox multiple ref="rupers"  ></Tree>
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
export default {
    methods:{
        create(){
            this.editRole={};
            this.$store.dispatch({
                type:'role/getrole',
                data:"-1"
            })
            this.showModal=true;
        },
        async save(){
            if(!!this.editRole.id){
                this.$refs.roleForm.validate(async (val)=>{
                    if(val){
                        var pc=[];
                        var p=this.$refs.rupers.getCheckedNodes();
                        
                        p.forEach(function(value,index,arr){
                            pc.push(value.id);
                        });
                        this.editRole.persIds=pc;
                        await this.$store.dispatch({
                            type:'role/update',
                            data:this.editRole
                        })
                        this.showEditModal=false;
                        this.$Message.success('保存成功！');
                        await this.getpage();
                    }
                })
                
            }else{
                this.$refs.newRoleForm.validate(async (val)=>{
                    if(val){
                        var pc=[];
                        var p=this.$refs.rcpers.getCheckedNodes();
                        
                        p.forEach(function(value,index,arr){
                            pc.push(value.id);
                        });
                        this.editRole.persIds=pc;
                        await this.$store.dispatch({
                            type:'role/create',
                            data:this.editRole
                        })
                        this.showModal=false;
                        this.$Message.success('添加成功！');
                        await this.getpage();
                    }
                })
            }
            
        },
        pageChange(page){
            this.$store.commit('role/setCurrentPage',page);
            this.getpage();
        },
        pagesizeChange(pagesize){
            this.$store.commit('role/setPageSize',pagesize);
            this.getpage();
        },
        async getpage(){
            await this.$store.dispatch({
                type:'role/getAll'
            })
        },
        
        onselectionchange(row){
            this.selectedData=row;
        },
        handleClickActionsDropdown(name){
            if(name==='Create'){
                this.create();
            }else if(name==='Refresh'){
                this.getpage();
                this.$Message.success('刷新成功！');

            }
        }
    },
    data(){
        return{
            editRole:{},
            showModal:false,
            showEditModal:false,
            selectedData:[],
            newRoleRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                code:[{required:true,message:'Code is required',trigger: 'blur'}],
            },            
            roleRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                code:[{required:true,message:'Code is required',trigger: 'blur'}],
            },
            columns:[
            
                    {
                        type: 'index',
                        width: 60,
                        align: 'center'
                    },
            {
                title:this.L('角色名称'),
                key:'name'
            },{
                title:this.L('角色编码'),
                key:'code'
            },{
                title: this.L('操作'),
                key: 'action',
                width:150,
                render:(h,params)=>{
                    return h('div',[
                        h('Button',{
                            props:{
                                type:'primary',
                                size:'small'
                            },
                            style:{
                                marginRight:'5px'
                            },
                            on:{
                                click:()=>{
                                    this.editRole=this.roles[params.index];
                                    this.$store.dispatch({
                                        type:'role/getrole',
                                        data:this.editRole.id
                                    });
                                    this.showEditModal=true;
                                }
                            }
                        },this.L('编辑')),
                        h('Button',{
                            props:{
                                type:'error',
                                size:'small'
                            },
                            on:{
                                click:async()=>{
                                    this.$Modal.confirm({
                                        title:this.L(''),
                                        content:this.L('删除这个角色吗？'),
                                        okText:this.L('是'),
                                        cancelText:this.L('否'),
                                        onOk:async()=>{
                                            await this.$store.dispatch({
                                                type:'role/delete',
                                                data:this.roles[params.index]
                                            })
                                            this.$Message.success('删除成功！');
                                            await this.getpage();
                                        }
                                    })
                                }
                            }
                        },this.L('删除'))
                    ])
                }
            }]
        }
    },
    computed:{
        roles(){
            return this.$store.state.role.roles;
        },
        permissions(){
            return this.$store.state.role.permissions;
        },
        totalCount(){
            return this.$store.state.role.totalCount;
        },
        currentPage(){
            return this.$store.state.role.currentPage;
        },
        pageSize(){
            return this.$store.state.role.pageSize;
        },
        pers(){
            return this.$store.state.role.pers;
        }
    },
    async created(){
        this.getpage();
        
    }
}
</script>



