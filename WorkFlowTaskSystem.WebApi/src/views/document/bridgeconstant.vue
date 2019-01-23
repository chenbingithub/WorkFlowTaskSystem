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
                <span slot="prepend">桥架编码:</span>
                <Button slot="append" type="info"  @click="seachKeyChange">搜索</Button>
            </Input>
            <Table  :columns="columns"  border :data="bridgeconstants"></Table>
            <Page :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage" show-sizer show-elevator show-total ></Page>
        </Card>
        <Modal v-model="showModal" :title="L('添加')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                    <Form ref="newbridgeconstantForm" :label-width="100" :rules="bridgeconstantRule" :model="editbridgeconstant">
                            
                            <FormItem :label="L('桥架编码')" prop="bridgeCode">
                                <Input v-model="editbridgeconstant.bridgeCode" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('通道类型')" prop="passageType">
                                <Input v-model="editbridgeconstant.passageType" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('类型')" prop="types">
                                <Input v-model="editbridgeconstant.types" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('系列')" prop="series">
                                <Input v-model="editbridgeconstant.series" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            
                            <FormItem :label="L('型号')" prop="version">
                                <Input v-model="editbridgeconstant.version" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('长')" prop="lenght" >
                                <Input v-model="editbridgeconstant.lenght"  number ></Input>
                            </FormItem>
                            <FormItem :label="L('宽')" prop="weight"  >
                                <Input v-model="editbridgeconstant.weight" number ></Input>
                            </FormItem>
                            <FormItem :label="L('高')" prop="height"  >
                                <Input v-model="editbridgeconstant.height" number ></Input>
                            </FormItem>
                            
                            <FormItem :label="L('桥架截面积')" prop="sectionalArea">
                                <Input v-model="editbridgeconstant.sectionalArea" disabled :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('容积率限值')" prop="plotRatioLimit">
                                <Input v-model="editbridgeconstant.plotRatioLimit" disabled :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('重量限值')" prop="weightLimit">
                                <Input v-model="editbridgeconstant.weightLimit" disabled :maxlength="32" :minlength="2"></Input>
                            </FormItem>                                
                </Form>                        
            </div>
            <div slot="footer">
                <Button @click="cancel">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
        <Modal v-model="showEditModal" :title="L('编辑')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                <Form ref="bridgeconstantForm" :label-width="100" :rules="bridgeconstantRule" :model="editbridgeconstant">
                            
                            <FormItem :label="L('桥架编码')" prop="bridgeCode">
                                <Input v-model="editbridgeconstant.bridgeCode" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('通道类型')" prop="passageType">
                                <Input v-model="editbridgeconstant.passageType" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('类型')" prop="types">
                                <Input v-model="editbridgeconstant.types" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('系列')" prop="series">
                                <Input v-model="editbridgeconstant.series" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('型号')" prop="version">
                                <Input v-model="editbridgeconstant.version" :maxlength="64" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('长')" prop="lenght" >
                                <Input v-model="editbridgeconstant.lenght"  number ></Input>
                            </FormItem>
                            <FormItem :label="L('宽')" prop="weight"  >
                                <Input v-model="editbridgeconstant.weight" number ></Input>
                            </FormItem>
                            <FormItem :label="L('高')" prop="height"  >
                                <Input v-model="editbridgeconstant.height" number ></Input>
                            </FormItem>
                            
                            <FormItem :label="L('桥架截面积')" prop="sectionalArea">
                                <Input v-model="editbridgeconstant.sectionalArea" disabled :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('容积率限值')" prop="plotRatioLimit">
                                <Input v-model="editbridgeconstant.plotRatioLimit" disabled :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('重量限值')" prop="weightLimit">
                                <Input v-model="editbridgeconstant.weightLimit" disabled :maxlength="32" :minlength="2"></Input>
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
            this.editbridgeconstant={};  
            this.editbridgeconstant.description=abp.randomNumber();
            this.showModal=true;
        },
        cancel(){
            if(!!this.editbridgeconstant.id){
                this.showEditModal=false;
                this.getpage();
            }else{
                this.showModal=false;
                this.getpage();
            }
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
            this.$store.commit('bridgeconstant/setSeachKey',this.seachKey);
            await this.$store.dispatch({
                type:'bridgeconstant/getAll'
            })
        },
        seachKeyChange(){
             this.$store.commit('bridgeconstant/setCurrentPage',1);
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
            seachKey:"",
            newbridgeconstantRule:{
                bridgeCode:[{required:true,message:'bridgeCode is required',trigger: 'blur'}],
                passageType:[{required:true,message:'passageType is required',trigger: 'blur'}],
                lenght:[{validator:abp.validateInteger,trigger: 'blur'}],
                weight:[{validator:abp.validateInteger,trigger: 'blur'}],
                height:[{validator:abp.validateInteger,trigger: 'blur'}],
            },            
            bridgeconstantRule:{
                bridgeCode:[{required:true,message:'bridgeCode is required',trigger: 'blur'}],
                passageType:[{required:true,message:'passageType is required',trigger: 'blur'}],
                lenght:[{validator:abp.validateInteger,trigger: 'blur'}],
                weight:[{validator:abp.validateInteger,trigger: 'blur'}],
                height:[{validator:abp.validateInteger,trigger: 'blur'}],
            },
            columns:[
            
                    {
                    title:this.L('序号'),
                        type: 'index',
                        width: 61,
                        align: 'center'
                    },
            {
                title:this.L('桥架编码'),
                 width: 160,
                key:'bridgeCode'
            },
            {
                title:this.L('类型'),
                key:'types'
            },
            {
                title:this.L('系列'),
                key:'series'
            },{
                title:this.L('型号'),
                key:'version'
            },{
                title:this.L('长'),
                key:'lenght'
            },{
                title:this.L('宽'),
                key:'weight'
            },{
                title:this.L('高'),
                key:'height'
            },{
                title:this.L('通道类型'),
                key:'passageType'
            },{
                title:this.L('桥架截面积(mm2)'),
                key:'sectionalArea',
                width: 160,
            },{
                title:this.L('容积率限值（%）'),
                 width: 160,
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
        },
        
    },
    async created(){
        this.getpage();
        
    }
}
</script>



