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
            <Input v-model="seachKey" @on-enter="seachKeyChange" >
                <span slot="prepend">电缆型号:</span>
                <Button slot="append" type="info"  @click="seachKeyChange">搜索</Button>
            </Input>
            <Table  :columns="columns"  border :data="cableconstants"></Table>
            <Page :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage" show-sizer show-elevator show-total ></Page>
        </Card>
        <Modal v-model="showModal" :title="L('添加')" @on-ok="save"  :okText="L('save')" :cancelText="L('cancel')">
            <div>
                       <Form ref="newcableconstantForm" label-position="top" :rules="cableconstantRule" :model="editcableconstant">
                            
                            <FormItem :label="L('电缆型号')" prop="version">
                                <Input v-model="editcableconstant.version" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('电缆规格')" prop="specification">
                                <Input v-model="editcableconstant.specification" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('外径(mm)')" prop="diameter" >
                                <Input v-model="editcableconstant.diameter" number ></Input>
                            </FormItem>
                            <FormItem :label="L('重量(kg/m)')" prop="weightLimit" >
                                <Input v-model="editcableconstant.weightLimit"  number ></Input>
                            </FormItem>
                            
                                              
                        </Form>
                            
            </div>
            <div slot="footer">
                <Button @click="cancel">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
        <Modal v-model="showEditModal" :title="L('编辑')" @on-ok="save"  :okText="L('save')" :cancelText="L('cancel')">
            <div>
                <Form ref="cableconstantForm" label-position="top" :rules="cableconstantRule" :model="editcableconstant">
                            
                            <FormItem :label="L('电缆型号')" prop="version">
                                <Input v-model="editcableconstant.version" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('电缆规格')" prop="specification">
                                <Input v-model="editcableconstant.specification" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('外径(mm)')" prop="diameter" >
                                <Input v-model="editcableconstant.diameter" number ></Input>
                            </FormItem>
                            <FormItem :label="L('重量(kg/m)')" prop="weightLimit" >
                                <Input v-model="editcableconstant.weightLimit"  number ></Input>
                            </FormItem>
                            
                                              
                        </Form>
            </div>
            <div slot="footer">
                <Button @click="cancel">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
    </div>
</template>
<script>
export default {
    methods:{
        create(){
            this.editcableconstant={};  
            this.editcableconstant.description=abp.randomNumber();
            this.showModal=true;
        },
        cancel(){
            if(!!this.editcableconstant.id){
                this.showEditModal=false;
                this.getpage();
            }else{
                this.showModal=false;
                this.getpage();
            }
        },
        async save(){
            if(!!this.editcableconstant.id){
                this.$refs.cableconstantForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'cableconstant/update',
                            data:this.editcableconstant
                        })
                        this.showEditModal=false;
                        this.$Message.success('保存成功！');
                        await this.getpage();
                    }
                })
                
            }else{
                this.$refs.newcableconstantForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'cableconstant/create',
                            data:this.editcableconstant
                        })
                        this.showModal=false;
                        this.$Message.success('添加成功！');
                        await this.getpage();
                    }
                })
            }
            
        },
        pageChange(page){
            this.$store.commit('cableconstant/setCurrentPage',page);
            this.getpage();
        },
        pagesizeChange(pagesize){
            this.$store.commit('cableconstant/setPageSize',pagesize);
            this.getpage();
        },
        async getpage(){
            this.$store.commit('cableconstant/setSeachKey',this.seachKey);
            await this.$store.dispatch({
                type:'cableconstant/getAll'
            })
        },
        seachKeyChange(){
            this.$store.commit('cableconstant/setCurrentPage',1);
            this.getpage();
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
                    type:'cableconstant/deleteAll'
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
            editcableconstant:{},
             persBtn:{
                create:true,//abp.auth.isGranted('Pages.cableconstants.Create'),
                update:true,//abp.auth.isGranted('Pages.cableconstants.Update'),
                delete:true,//abp.auth.isGranted('Pages.cableconstants.Delete'),
            },
            showModal:false,
            showEditModal:false,
            selectedData:[],
            seachKey:"",
            newcableconstantRule:{
                version:[{required:true,message:'version is required',trigger: 'blur'}],
                specification:[{required:true,message:'specification is required',trigger: 'blur'}],
                diameter:[{validator:abp.validateInteger,trigger: 'blur'}],
                weightLimit:[{validator:abp.validateInteger,trigger: 'blur'}],
            },            
            cableconstantRule:{
                version:[{required:true,message:'version is required',trigger: 'blur'}],
                specification:[{required:true,message:'specification is required',trigger: 'blur'}],
                diameter:[{validator:abp.validateInteger,trigger: 'blur'}],
                weightLimit:[{validator:abp.validateInteger,trigger: 'blur'}],
            },
            columns:[
            
                    {
                    title:this.L('序号'),
                        type: 'index',
                        width: 61,
                        align: 'center'
                    },
            {
                title:this.L('电缆型号'),
                key:'version'
            },{
                title:this.L('电缆规格'),
                key:'specification'
            },{
                title:this.L('外径(mm)'),
                key:'diameter'
            },{
                title:this.L('重量(kg/m)'),
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
                                                    this.editcableconstant=this.cableconstants[params.index];
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
                                                                type:'cableconstant/delete',
                                                                data:this.cableconstants[params.index]
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
        cableconstants(){
            return this.$store.state.cableconstant.cableconstants;
        },
        
        totalCount(){
            return this.$store.state.cableconstant.totalCount;
        },
        currentPage(){
            return this.$store.state.cableconstant.currentPage;
        },
        pageSize(){
            return this.$store.state.cableconstant.pageSize;
        }
    },
    async created(){
        this.getpage();
        
    }
}
</script>



