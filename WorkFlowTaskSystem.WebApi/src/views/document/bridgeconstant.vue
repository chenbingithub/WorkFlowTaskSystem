<template>
    <div>
        <Card>
            <p slot="title">{{'数据管理'|l}}</p>
            <Dropdown slot="extra"  @on-click="handleClickActionsDropdown">
                <a href="javascript:void(0)">
                    {{'操作'|l}}
                    <Icon type="android-more-vertical"></Icon>
                </a>
                <DropdownMenu slot="list">
                        
                    <DropdownItem name='Refresh'>{{'刷新' | l}}</DropdownItem>
                    <DropdownItem name='Create' v-if="persBtn.create" >{{'添加' | l}}</DropdownItem>
                    <DropdownItem name='DeleteAll' v-if="persBtn.create" >{{'删除所有' | l}}</DropdownItem>
                </DropdownMenu>
            </Dropdown>
            <Table  :columns="columns" height="400" border :data="bridgeconstants"></Table>
            <Page :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage" show-sizer show-elevator show-total ></Page>
        </Card>
        <Modal v-model="showModal" :title="L('添加')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                    <Form ref="bridgeconstantForm" label-position="top" :rules="bridgeconstantRule" :model="editbridgeconstant">
                            <FormItem :label="L('流水号')" prop="description">
                                <Input v-model="editbridgeconstant.description" disabled  ></Input>
                            </FormItem>
                            <FormItem :label="L('桥架编码')" prop="bridgeCode">
                                <Input v-model="editbridgeconstant.bridgeCode" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('通道类型')" prop="passageType">
                                <Input v-model="editbridgeconstant.passageType" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('桥架截面积')" prop="sectionalArea">
                                <Input v-model="editbridgeconstant.sectionalArea" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('容积率限值')" prop="plotRatioLimit">
                                <Input v-model="editbridgeconstant.plotRatioLimit" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('重量限值')" prop="weightLimit">
                                <Input v-model="editbridgeconstant.weightLimit" :maxlength="32" :minlength="2"></Input>
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
                <Form ref="bridgeconstantForm" label-position="top" :rules="bridgeconstantRule" :model="editbridgeconstant">
                            <FormItem :label="L('流水号')" prop="description">
                                <Input v-model="editbridgeconstant.description" disabled  ></Input>
                            </FormItem>
                            <FormItem :label="L('桥架编码')" prop="bridgeCode">
                                <Input v-model="editbridgeconstant.bridgeCode" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('通道类型')" prop="passageType">
                                <Input v-model="editbridgeconstant.passageType" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('桥架截面积')" prop="sectionalArea">
                                <Input v-model="editbridgeconstant.sectionalArea" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('容积率限值')" prop="plotRatioLimit">
                                <Input v-model="editbridgeconstant.plotRatioLimit" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('重量限值')" prop="weightLimit">
                                <Input v-model="editbridgeconstant.weightLimit" :maxlength="32" :minlength="2"></Input>
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
export default {
    methods:{
        create(){
            this.editbridgeconstant={};  
            this.editbridgeconstant.description=abp.randomNumber();
            this.showModal=true;
        },
        async save(){
            if(!!this.editbridgeconstant.id){
                this.$refs.bridgeconstantForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'bridgeconstant/update',
                            data:this.editbridgeconstant
                        })
                        this.showEditModal=false;
                        this.$Message.success('保存成功！');
                        await this.getpage();
                    }
                })
                
            }else{
                this.$refs.newbridgeconstantForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'bridgeconstant/create',
                            data:this.editbridgeconstant
                        })
                        this.showModal=false;
                        this.$Message.success('添加成功！');
                        await this.getpage();
                    }
                })
            }
            
        },
        pageChange(page){
            this.$store.commit('bridgeconstant/setCurrentPage',page);
            this.getpage();
        },
        pagesizeChange(pagesize){
            this.$store.commit('bridgeconstant/setPageSize',pagesize);
            this.getpage();
        },
        async getpage(){
            await this.$store.dispatch({
                type:'bridgeconstant/getAll'
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

            }else if(name=='DeleteAll'){
                var $this=this;
                this.$store.dispatch({
                    type:'bridgeconstant/deleteAll'
                }).then(function (response) {
                    $this.getpage();
                    $this.$Message.success('刷新成功！');
                }).catch(function (error) {
                    console.log(error);
                    
                });
                
            }
        }
    },
    data(){
        return{
            editbridgeconstant:{},
             persBtn:{
                create:true,//abp.auth.isGranted('Pages.bridgeconstants.Create'),
                update:true,//abp.auth.isGranted('Pages.bridgeconstants.Update'),
                delete:true,//abp.auth.isGranted('Pages.bridgeconstants.Delete'),
            },
            showModal:false,
            showEditModal:false,
            selectedData:[],
            newbridgeconstantRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                code:[{required:true,message:'Code is required',trigger: 'blur'}],
            },            
            bridgeconstantRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                code:[{required:true,message:'Code is required',trigger: 'blur'}],
            },
            columns:[
            
                    {
                        type: 'index',
                        width: 60,
                        align: 'center'
                    },{
                title:this.L('流水号'),
                key:'description'
            },
            {
                title:this.L('桥架编码'),
                key:'bridgeCode'
            },{
                title:this.L('通道类型'),
                key:'passageType'
            },{
                title:this.L('桥架截面积'),
                key:'sectionalArea'
            },{
                title:this.L('容积率限值'),
                key:'plotRatioLimit'
            },{
                title:this.L('重量限值'),
                key:'weightLimit'
            },{
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
                                                    this.editbridgeconstant=this.bridgeconstants[params.index];
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
                                                        content:this.L('删除这个数据吗？'),
                                                        okText:this.L('是'),
                                                        cancelText:this.L('否'),
                                                        onOk:async()=>{
                                                            await this.$store.dispatch({
                                                                type:'bridgeconstant/delete',
                                                                data:this.bridgeconstants[params.index]
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
            }]
        }
    },
    computed:{
        bridgeconstants(){
            return this.$store.state.bridgeconstant.bridgeconstants;
        },
        
        totalCount(){
            return this.$store.state.bridgeconstant.totalCount;
        },
        currentPage(){
            return this.$store.state.bridgeconstant.currentPage;
        },
        pageSize(){
            return this.$store.state.bridgeconstant.pageSize;
        }
    },
    async created(){
        this.getpage();
        
    }
}
</script>



